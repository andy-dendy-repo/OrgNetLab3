using Dapper;
using OrgNetLab3.Data.Core;
using OrgNetLab3.Data.Entity;
using OrgNetLab3.Data.Query;
using System.Data;

namespace OrgNetLab3.Data.Services
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IDbConnection connection) : base(connection)
        {

        }

        protected override void SetPrimaryKeysBeforeInsert(User entity)
        {
            entity.Id = Guid.NewGuid();
        }

        public async Task Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await Create(user);
        }

        public async Task UpdateAndHashPassword(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await Update(user);
        }

        public async Task<IList<User>> GetStudentsByLesson(Guid lessonId)
        {
            var procedure = $"[GetStudentsByLesson]";
            await Log(procedure);
            return (await _connection.QueryAsync<User>(procedure, new { LessonId = lessonId }, commandType: CommandType.StoredProcedure))
                .ToList();
        }

        public async Task<User> GetUserByEmail(string email, string password)
        {
            var procedure = $"[GetUserByEmail]";
            await Log(procedure);
            User user = await _connection.QueryFirstOrDefaultAsync<User>(procedure, new { Email = email }, commandType: CommandType.StoredProcedure);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                return user;
            else
                return null;
        }

        public async Task<ScheduleModel> GetSchedule(Guid userId)
        {
            string sql = $@"select g.Code Name, l.Id LessonId, u.Email TeacherEmail, l.Start Start, s.Name SubjectName from [Group] g
                            join [StudentGroup] sg on sg.GroupId = g.Id 
                            join [Lesson] l on l.GroupId = g.Id 
                            join [Subject] s on s.Id = l.SubjectId 
                            join [User] u on u.Id = l.TeacherId 
                            where sg.StudentId = '{userId}' 
                            group by g.Code, u.Email, l.Id, l.Start, s.Name";

            await Log(sql);

            IList<GroupModel> groups = new List<GroupModel>(
                await _connection.QueryAsync<GroupModel, LessonModel, GroupModel>(
                    sql
                    , (x, y) =>
                    {
                        x.Lessons.Add(y);
                        return x;
                    }
                    , splitOn: "LessonId")
                );

            var result = groups.GroupBy(p => p.Name).Select(g =>
            {
                var groupedPost = g.First();
                groupedPost.Lessons = g.Select(p => p.Lessons.Single()).ToList();
                return groupedPost;
            }).ToList();

            return new ScheduleModel { Groups = result };
        }
    }
}
