using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace PlanoriaCapstone.DTOs.Flashcards.Decks.Requests
{
    public class ImportDeckRequestDto
    {
        public string Format { get; set; }
        public IFormFile File { get; set; }
        public Guid TargetCourseId { get; set; }
        public bool ReplaceDuplicates { get; set; }
    }
}