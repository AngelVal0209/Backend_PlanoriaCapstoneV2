using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Requests
{
    public class DuplicateQuizRequestDto
    {
        public string NewTitle { get; set; }
        public int TargetCourseId { get; set; }
    }
}
