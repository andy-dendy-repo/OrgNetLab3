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

        public class Result
        {
            public string Status { get; set; }
        }

        public async Task<bool> SetFile(Guid lessonId, byte[] file)
        {
            
            string sql = @$"
            SET TRANSACTION ISOLATION LEVEL Serializable;
            declare @res nvarchar(100) = 'False'
            if exists(select * from [Lesson] where Id = '{lessonId}' and [File] is NULL)
            begin;
                update [Lesson] set [File] = @Bytes where Id = '{lessonId}';
                set @res = 'True'
            end;
            select @res as Status;
            ";

            await Log(sql);

            var result = await _connection.QueryFirstAsync<Result>(sql, new { Bytes = file });

            return result.Status == "True";
        }
    }
}
