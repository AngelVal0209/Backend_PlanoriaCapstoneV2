using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Courses.Responses
{
    public class CourseStatsResponseDto
    {
        public int TotalFlashcards { get; set; }
        public int FlashcardsMastered { get; set; }
        public int TotalQuizzes { get; set; }
        public int QuizzesPassed { get; set; }
        public decimal AverageQuizScore { get; set; }
        public int StudyTimeHours { get; set; }
        public DateTime? LastActiveAt { get; set; }
    }
}