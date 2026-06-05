using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Courses.Responses
{
    public class CourseListResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorHex { get; set; }
        public DateTime? ExamDate { get; set; }
        public decimal ProgressPercentage { get; set; }
        public bool IsArchived { get; set; }
        public DateTime? LastStudiedAt { get; set; }
    }
}