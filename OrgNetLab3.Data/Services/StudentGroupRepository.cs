using OrgNetLab3.Data.Core;
using OrgNetLab3.Data.Entity;
using System.Data;

namespace OrgNetLab3.Data.Services
{
    public class StudentGroupRepository : BaseRepository<StudentGroup>
    {
        public StudentGroupRepository(IDbConnection connection) : base(connection)
        {

        }

        protected override void SetPrimaryKeysBeforeInsert(StudentGroup entity)
        {
            
        }

        protected override object GetKeysContainer(object[] keys)
        {
            return keys == null ? new { GroupId = (string)null, StudentId = (string)null } : new { GroupId = keys[0], StudentId = keys[1] };
        }
    }
}
