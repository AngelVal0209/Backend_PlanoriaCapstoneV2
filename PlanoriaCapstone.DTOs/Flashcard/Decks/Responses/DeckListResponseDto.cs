using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Decks.Responses
{
    public class DeckListResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalCards { get; set; }
        public decimal MasteredPercentage { get; set; }
        public DateTime? LastStudiedAt { get; set; }
        public int DueCardsCount { get; set; }
    }
}