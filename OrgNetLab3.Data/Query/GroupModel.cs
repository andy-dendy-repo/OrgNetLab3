using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgNetLab3.Data.Query
{
    public class GroupModel
    {
        public string Name { get; set; }

        public IList<LessonModel> Lessons { get; set; } = new List<LessonModel>();
    }
}
