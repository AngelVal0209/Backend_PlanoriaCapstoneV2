using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progress.Responses.Exam
{
    public class CourseExamProgressResponseDto
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime? ExamDate { get; set; }
        public int DaysRemaining { get; set; }
        public decimal ExamReadinessScore { get; set; }
        public decimal TotalProgressPercentage { get; set; }
        public int RequiredDailyCards { get; set; }
        public int RequiredDailyQuizzes { get; set; }
        public bool IsOnTrack { get; set; }
    }
}
