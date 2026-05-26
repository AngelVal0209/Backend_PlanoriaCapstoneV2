using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class ArchivoSubido
    {
        public int IdArchivo { get; set; }

        public int IdUsuario { get; set; }

        public string NombreArchivo { get; set; } = string.Empty;

        public string UrlArchivo { get; set; } = string.Empty;

        public string TipoArchivo { get; set; } = string.Empty;

        public decimal? TamanoMB { get; set; }

        public DateTime FechaSubida { get; set; }

        public string Estado { get; set; } = "ACTIVO";

        public Usuario? Usuario { get; set; }

        public ICollection<AnalisisIA>? AnalisisIA { get; set; }

        //Relacion con la clase Curso 
        public int? IdCursos { get; set; }

        public Curso? Curso { get; set; }
    }
}
