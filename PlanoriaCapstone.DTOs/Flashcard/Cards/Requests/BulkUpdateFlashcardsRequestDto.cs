using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Cards.Requests
{
    public class FlashcardUpdateItem
    {
        public int Id { get; set; }
        public UpdateFlashcardRequestDto Data { get; set; }
    }

    public class BulkUpdateFlashcardsRequestDto
    {
        public List<FlashcardUpdateItem> Updates { get; set; }
    }
}