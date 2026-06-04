using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progress.Responses.Flashcards
{
    public class FlashcardProgressResponseDto
    {
        public Guid DeckId { get; set; }
        public string DeckName { get; set; }
        public int TotalCards { get; set; }
        public int StudiedCount { get; set; }
        public int MasteredCount { get; set; }
        public int LearningCount { get; set; }
        public int NotStartedCount { get; set; }
        public decimal MasteryPercentage { get; set; }
        public DateTime? LastStudiedAt { get; set; }
    }
}
