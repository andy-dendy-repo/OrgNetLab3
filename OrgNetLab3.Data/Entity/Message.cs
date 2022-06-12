using OrgNetLab3.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgNetLab3.Data.Entity
{
    public class Message : PkNonClustered<Guid>
    {
        public Guid From { get; set; }

        public Guid To { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
