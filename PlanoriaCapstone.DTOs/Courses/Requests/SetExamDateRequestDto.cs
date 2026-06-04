using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Courses.Requests
{
    public class SetExamDateRequestDto
    {
        public DateTime ExamDate { get; set; }
        public string ExamTime { get; set; }
        public bool NotifyMe { get; set; }
    }
}