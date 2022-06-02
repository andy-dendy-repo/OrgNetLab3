using OrgNetLab3.Data.Core;
using OrgNetLab3.Data.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgNetLab3.Data.Entity
{
    public class Lesson : PkNonClustered<Guid>
    {
        public Guid GroupId { get; set; }

        public Guid SubjectId { get; set; }

        public Guid TeacherId { get; set; }

        public DateTime Start { get; set; }

        public Status Status { get; set; }
    }
}
