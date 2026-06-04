using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Cards.Responses
{
    public class FlashcardResponseDto
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Hint { get; set; }
        public string Difficulty { get; set; }
        public List<string> Tags { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; }
        public Guid DeckId { get; set; }
        public DateTime? LastReviewedAt { get; set; }
        public DateTime? NextReviewDate { get; set; }
        public int RepetitionCount { get; set; }
        public decimal EaseFactor { get; set; }
    }
}