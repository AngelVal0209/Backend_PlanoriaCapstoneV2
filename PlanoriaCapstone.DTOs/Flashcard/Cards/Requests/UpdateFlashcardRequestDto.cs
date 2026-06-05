using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Cards.Requests
{
    public class UpdateFlashcardRequestDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Hint { get; set; }
        public string Difficulty { get; set; }
        public List<string> Tags { get; set; }
        public int? Position { get; set; }
        public bool? IsActive { get; set; }
    }
}