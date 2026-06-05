using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Decks.Requests
{
    public class DuplicateDeckRequestDto
    {
        public string NewName { get; set; }
        public Guid TargetCourseId { get; set; }
    }
}