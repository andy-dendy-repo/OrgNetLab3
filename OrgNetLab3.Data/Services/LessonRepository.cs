using Dapper;
using OrgNetLab3.Data.Core;
using OrgNetLab3.Data.Entity;
using System.Data;

namespace OrgNetLab3.Data.Services
{
    public class LessonRepository : BaseRepository<Lesson>
    {
        private class FileContainer
        {
            public byte[] File { get; set; }
        }

        public LessonRepository(IDbConnection connection) : base(connection)
        {

        }

        protected override void SetPrimaryKeysBeforeInsert(Lesson entity)
        {
            entity.Id = Guid.NewGuid();
        }

        public async Task<byte[]> GetFile(Guid lessonId)
        {
            string sql = $"select [File] from [Lesson] where Id = '{lessonId}'";
            await Log(sql);
            var result = await _connection.QueryFirstAsync<FileContainer>(sql);

            return result.File;
        }

        public async Task SetFile(Guid lessonId, byte[] file)
        {
            
            string sql = $"update [Lesson] set [File] = @Bytes where Id = '{lessonId}'";
            await Log(sql);
            await _connection.ExecuteAsync(sql, new { Bytes = file });
        }
    }
}
