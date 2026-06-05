using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Responses
{
    public class AnswerResponseDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public OptionResponseDto SelectedOption { get; set; }
        public string ShortAnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public decimal PointsEarned { get; set; }
        public OptionResponseDto CorrectAnswer { get; set; }
    }
}
