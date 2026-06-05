using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Decks.Requests
{
    public class ShareDeckRequestDto
    {
        public Guid UserId { get; set; }
        public string Permission { get; set; }
    }
}