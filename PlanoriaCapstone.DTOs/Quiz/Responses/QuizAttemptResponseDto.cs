using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Responses
{
    public class QuizAttemptResponseDto
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public decimal? ScorePercentage { get; set; }
        public bool? Passed { get; set; }
        public int? TimeSpentSeconds { get; set; }
        public int AnswersCount { get; set; }
        public int CorrectAnswersCount { get; set; }
    }
}
