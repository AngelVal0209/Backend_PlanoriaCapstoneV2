using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class ProgresoFlashcard
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public int IdFlashcard { get; set; }

        public bool Completado { get; set; }

        public int VecesRepasada { get; set; }

        // =====================================
        // RELACIONES
        // =====================================

        public Usuario? Usuario { get; set; }

        public Flashcard? Flashcard { get; set; }
    }
}