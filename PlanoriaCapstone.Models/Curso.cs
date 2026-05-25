using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class Curso 
    {
        public int IdCurso { get; set; }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }    
        public DateTime? FechaCreacion { get; set; }
        
        //Relaciones 
        public Usuario? Usuario { get; set; }
        public ICollection<ArchivoSubido>? Archivos { get; set; }

    }
}
