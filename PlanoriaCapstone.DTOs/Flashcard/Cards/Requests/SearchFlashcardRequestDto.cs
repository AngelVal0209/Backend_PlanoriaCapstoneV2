using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Cards.Requests
{
    public class SearchFlashcardRequestDto
    {
        public string Query { get; set; }
        public int? DeckId { get; set; }
        public List<string> Tags { get; set; }
        public string Difficulty { get; set; }
        public bool? IsActive { get; set; }
        public int Limit { get; set; } = 50;
    }
}