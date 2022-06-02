using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgNetLab3.Data.Query
{
    public class LessonModel
    {
        public Guid LessonId { get; set; }

        public string TeacherEmail { get; set; }

        public string SubjectName { get; set; }

        public DateTime Start { get; set; }
    }
}
