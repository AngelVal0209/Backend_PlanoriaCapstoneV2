using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Cards.Responses
{
    public class DueCardsResponseDto
    {
        public Guid DeckId { get; set; }
        public int TotalDue { get; set; }
        public int OverdueCount { get; set; }
        public int DueTodayCount { get; set; }
        public List<FlashcardResponseDto> Cards { get; set; }
    }
}