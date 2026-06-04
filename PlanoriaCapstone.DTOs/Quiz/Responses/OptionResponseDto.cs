using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Responses
{
    public class OptionResponseDto
    {
        public Guid Id { get; set; }
        public string OptionText { get; set; }
        public bool? IsCorrect { get; set; }
        public int OrderPosition { get; set; }
    }
}
