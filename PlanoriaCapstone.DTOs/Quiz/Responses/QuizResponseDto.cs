using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Responses
{
    public class QuizResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public int TotalQuestions { get; set; }
        public decimal PassingScore { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public bool ShuffleQuestions { get; set; }
        public bool ShuffleOptions { get; set; }
        public int AttemptsAllowed { get; set; }
        public int UserAttempts { get; set; }
        public decimal? BestScore { get; set; }
        public decimal? AverageScore { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? LastAttemptAt { get; set; }
    }
}
