using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Requests
{
    public class CreateQuizRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public decimal PassingScore { get; set; } = 70.00m;
        public int? TimeLimitMinutes { get; set; }
        public bool ShuffleQuestions { get; set; } = false;
        public bool ShuffleOptions { get; set; } = false;
        public int AttemptsAllowed { get; set; } = 0;
    }
}
