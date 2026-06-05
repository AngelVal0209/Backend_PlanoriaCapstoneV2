using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Requests
{
    public class UpdateQuizRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? PassingScore { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public bool? ShuffleQuestions { get; set; }
        public bool? ShuffleOptions { get; set; }
        public int? AttemptsAllowed { get; set; }
        public bool? IsActive { get; set; }
    }
}
