using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progress.Responses.Quizzes
{
    public class QuizProgressResponseDto
    {
        public Guid QuizId { get; set; }
        public string QuizTitle { get; set; }
        public int TotalAttempts { get; set; }
        public decimal? BestScore { get; set; }
        public decimal? AverageScore { get; set; }
        public DateTime? LastAttemptDate { get; set; }
        public int PassedCount { get; set; }
        public bool RecommendedRetry { get; set; }
    }
}
