using OrgNetLab3.Data.Core;
using OrgNetLab3.Data.Entity;
using System.Data;

namespace OrgNetLab3.Data.Services
{
    public class GroupRepository : BaseRepository<Group>
    {
        public GroupRepository(IDbConnection connection) : base(connection)
        {

        }

        protected override void SetPrimaryKeysBeforeInsert(Group entity)
        {
            entity.Id = Guid.NewGuid();
        }
    }
}
