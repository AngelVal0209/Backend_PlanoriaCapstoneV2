using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Courses.Responses
{
    public class CourseExamResponseDto
    {
        public DateTime? ExamDate { get; set; }
        public string ExamTime { get; set; }
        public int? DaysRemaining { get; set; }
        public bool IsOverdue { get; set; }
        public decimal ReadinessScore { get; set; }
    }
}