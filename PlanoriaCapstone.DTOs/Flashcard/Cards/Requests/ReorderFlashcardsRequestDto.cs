using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Cards.Requests
{
    public class CardOrderItem
    {
        public int Id { get; set; }
        public int Position { get; set; }
    }

    public class ReorderFlashcardsRequestDto
    {
        public List<CardOrderItem> CardOrder { get; set; }
    }
}