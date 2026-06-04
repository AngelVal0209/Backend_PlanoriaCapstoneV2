using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Users.Requests
{
    public class ExportDataRequestDto
    {
        public string Format { get; set; }
        public bool IncludeFlashcards { get; set; }
        public bool IncludeQuizzes { get; set; }
        public bool IncludeProgress { get; set; }
    }
}