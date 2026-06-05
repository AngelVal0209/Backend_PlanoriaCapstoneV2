using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Requests
{
    public class CreateQuestionRequestDto
    {
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string Explanation { get; set; }
        public decimal Points { get; set; } = 1.00m;
        public int OrderPosition { get; set; }
        public List<CreateOptionRequestDto> Options { get; set; }
    }
}
