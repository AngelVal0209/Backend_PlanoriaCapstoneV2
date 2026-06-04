using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Decks.Responses
{
    public class DeckResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public int TotalCards { get; set; }
        public bool SpacedRepetitionEnabled { get; set; }
        public int MasteredCards { get; set; }
        public int LearningCards { get; set; }
        public int NotStudiedCards { get; set; }
        public decimal ProgressPercentage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}