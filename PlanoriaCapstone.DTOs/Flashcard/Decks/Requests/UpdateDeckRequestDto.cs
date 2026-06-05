using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Decks.Requests
{
    public class UpdateDeckRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? SpacedRepetitionEnabled { get; set; }
        public bool? IsArchived { get; set; }
    }
}