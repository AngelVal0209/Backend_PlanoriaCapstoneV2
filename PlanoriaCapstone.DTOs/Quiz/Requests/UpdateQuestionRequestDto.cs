using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Requests
{
    public class UpdateQuestionRequestDto
    {
        public string QuestionText { get; set; }
        public string Explanation { get; set; }
        public decimal? Points { get; set; }
        public int? OrderPosition { get; set; }
        public bool? IsActive { get; set; }
        public List<UpdateOptionRequestDto> Options { get; set; }
    }
}
