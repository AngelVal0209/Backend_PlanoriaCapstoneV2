using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Study.Responses
{
    public class StudySessionResponseDto
    {
        public Guid Id { get; set; }
        public Guid DeckId { get; set; }
        public string DeckName { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int CardsReviewed { get; set; }
        public int CardsKnown { get; set; }
        public int CardsUnknown { get; set; }
        public string SessionType { get; set; }
        public decimal PerformanceScore { get; set; }
    }
}