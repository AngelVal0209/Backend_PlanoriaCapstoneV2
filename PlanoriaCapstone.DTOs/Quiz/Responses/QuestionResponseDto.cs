using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Responses
{
    public class QuestionResponseDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string Explanation { get; set; }
        public decimal Points { get; set; }
        public int OrderPosition { get; set; }
        public List<OptionResponseDto> Options { get; set; }
    }
}
