using OrgNetLab3.Data.Core;
using OrgNetLab3.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgNetLab3.Data.Services
{
    public class MessageRepository : BaseRepository<Message>
    {
        public MessageRepository(IDbConnection connection) : base(connection)
        {

        }

        protected override void SetPrimaryKeysBeforeInsert(Message entity)
        {
        }
    }
}
