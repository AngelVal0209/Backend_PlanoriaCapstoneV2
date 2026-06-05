using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progress.Responses.Flashcards
{
    public class WeeklyFlashcardProgressResponseDto
    {
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
        public int CardsReviewed { get; set; }
        public int NewCardsLearned { get; set; }
        public int CardsMastered { get; set; }
        public decimal AverageEaseFactor { get; set; }
    }
}
