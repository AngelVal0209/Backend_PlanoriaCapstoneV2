using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Study.Requests
{
    public class ScheduleReviewRequestDto
    {
        public int FlashcardId { get; set; }
        public DateTime? ForceDate { get; set; }
    }
}