using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Study.Requests
{
    public class SubmitFlashcardAnswerRequestDto
    {
        public int FlashcardId { get; set; }
        public int SessionId { get; set; }
        public bool KnewIt { get; set; }
        public int ResponseTimeMs { get; set; }
    }
}