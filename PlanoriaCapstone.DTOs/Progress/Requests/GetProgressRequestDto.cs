using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progress.Requests
{
    public class GetProgressRequestDto
    {
        public int? CourseId { get; set; }
        public int? DeckId { get; set; }
        public int? QuizId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
