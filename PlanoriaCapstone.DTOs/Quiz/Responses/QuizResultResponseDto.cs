using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Responses
{
    public class QuizResultResponseDto
    {
        public QuizAttemptResponseDto Attempt { get; set; }
        public List<AnswerResponseDto> Answers { get; set; }
        public string FeedbackSummary { get; set; }
        public List<string> WeakTopics { get; set; }
        public List<string> Recommendations { get; set; }
    }
}
