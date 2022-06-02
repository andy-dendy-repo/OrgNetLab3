using OrgNetLab3.Data.Core;
using OrgNetLab3.Data.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgNetLab3.Data.Entity
{
    public class User : PkNonClustered<Guid>
    {
        public DateTime DateOfBirth { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
