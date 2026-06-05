using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Study.Requests
{
    public class StartStudySessionRequestDto
    {
        public int DeckId { get; set; }
        public string SessionType { get; set; } = "normal";
        public List<int> IncludeCards { get; set; }
    }
}