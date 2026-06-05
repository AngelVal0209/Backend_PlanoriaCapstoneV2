using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Cards.Responses
{
    public class SpacedRepetitionSettingsResponseDto
    {
        public Guid UserId { get; set; }
        public Guid? DeckId { get; set; }
        public int InitialInterval { get; set; }
        public int MaxInterval { get; set; }
        public decimal EasyBonus { get; set; }
        public decimal HardPenalty { get; set; }
        public List<int> CustomIntervals { get; set; }
    }
}