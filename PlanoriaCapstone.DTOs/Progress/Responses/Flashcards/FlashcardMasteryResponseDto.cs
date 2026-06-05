using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progress.Responses.Flashcards
{
    public class FlashcardMasteryResponseDto
    {
        public int FlashcardId { get; set; }
        public string Question { get; set; }
        public decimal EaseFactor { get; set; }
        public int RepetitionCount { get; set; }
        public DateTime? LastReviewDate { get; set; }
        public DateTime? NextReviewDate { get; set; }
        public string MasteryLevel { get; set; }
    }
}
