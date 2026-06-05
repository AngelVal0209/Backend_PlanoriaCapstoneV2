using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Requests
{
    public class CreateOptionRequestDto
    {
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public int OrderPosition { get; set; }
    }
}
