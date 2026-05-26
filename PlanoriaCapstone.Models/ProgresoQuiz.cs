using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class ProgresoQuiz
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public int IdQuiz { get; set; }

        public decimal Puntaje { get; set; }

        public bool Completado { get; set; }

        public DateTime FechaRealizacion { get; set; }

        // =====================================
        // RELACIONES
        // =====================================

        public Usuario? Usuario { get; set; }

        public Quiz? Quiz { get; set; }
    }
}