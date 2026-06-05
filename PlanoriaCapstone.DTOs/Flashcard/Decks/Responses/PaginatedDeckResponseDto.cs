using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcards.Decks.Responses
{
    public class PaginatedDeckResponseDto
    {
        public List<DeckListResponseDto> Data { get; set; }
        public int Total { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int TotalPages { get; set; }
    }
}