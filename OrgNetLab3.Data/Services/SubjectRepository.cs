using OrgNetLab3.Data.Core;
using OrgNetLab3.Data.Entity;
using OrgNetLab3.Data.Entity.Enums;
using System.Data;

namespace OrgNetLab3.Data.Services
{
    public class SubjectRepository : BaseRepository<Subject>
    {
        public SubjectRepository(IDbConnection connection) : base(connection)
        {

        }

        protected override void SetPrimaryKeysBeforeInsert(Subject entity)
        {
            entity.Id = Guid.NewGuid();
        }
    }
}
