using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Study.Requests
{
    public class GetDueCardsRequestDto
    {
        public Guid DeckId { get; set; }
        public int Limit { get; set; } = 20;
        public bool IncludeOverdue { get; set; } = true;
    }
}