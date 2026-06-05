using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Requests
{
    public class SubmitQuizRequestDto
    {
        public int AttemptId { get; set; }
        public List<SubmitAnswerRequestDto> Answers { get; set; }
    }
}
