using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Cards.Requests
{
    public class BulkCreateFlashcardsRequestDto
    {
        public Guid DeckId { get; set; }
        public List<CreateFlashcardRequestDto> Cards { get; set; }
    }
}