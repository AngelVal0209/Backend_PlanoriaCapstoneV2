using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcard
{
    public class ResponderFlashcardDTO
    {
        public int IdFlashcard { get; set; }

        public bool Correcta { get; set; }

        public int TiempoRespuestaSegundos { get; set; }
    }
}
