using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Courses.Responses
{
    public class CourseResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ExamDate { get; set; }
        public string ExamTime { get; set; }
        public string ColorHex { get; set; }
        public bool IsArchived { get; set; }
        public int TotalFlashcards { get; set; }
        public int TotalQuizzes { get; set; }
        public decimal ProgressPercentage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}