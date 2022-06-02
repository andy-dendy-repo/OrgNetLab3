﻿using OrgNetLab3.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgNetLab3.Data.Entity
{
    public class Group : PkNonClustered<Guid>
    {
        public string Code { get; set; }
    }
}
