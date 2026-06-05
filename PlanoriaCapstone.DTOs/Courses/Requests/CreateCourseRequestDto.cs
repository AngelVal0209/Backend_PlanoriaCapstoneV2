using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Courses.Requests
{
    public class CreateCourseRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ExamDate { get; set; }
        public string ExamTime { get; set; }
        public string ColorHex { get; set; } = "#3498db";
    }
}