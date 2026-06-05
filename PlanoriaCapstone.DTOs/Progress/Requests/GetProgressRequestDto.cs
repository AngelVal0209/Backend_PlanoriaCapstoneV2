using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progress.Requests
{
    public class GetProgressRequestDto
    {
        public Guid? CourseId { get; set; }
        public Guid? DeckId { get; set; }
        public Guid? QuizId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
