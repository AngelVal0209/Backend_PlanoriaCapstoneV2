using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Apellido { get; set; }

        public string Correo { get; set; } = string.Empty;

        public string? PasswordHash { get; set; }

        public string? GoogleId { get; set; }

        public string Provider { get; set; } = "LOCAL";

        public string? FotoPerfilUrl { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime? UltimoAcceso { get; set; }

        public int RachaDias { get; set; }

        public int Puntos { get; set; }

        public int Nivel { get; set; }

        public bool Estado { get; set; }

        public int IdRol { get; set; }

        public Rol? Rol { get; set; }

        public ICollection<ArchivoSubido>? ArchivosSubidos { get; set; }

        public ICollection<HistorialFlashcard>? HistorialFlashcards { get; set; }

        public ICollection<HistorialQuiz>? HistorialQuizzes { get; set; }

        public ICollection<RachaUsuario>? RachasUsuario { get; set; }

        public ICollection<Auditoria>? Auditorias { get; set; }

        public ICollection<ProgresoArchivo>? ProgresoArchivos { get; set; }

        public EstadisticaIA? EstadisticaIA { get; set; }

        public ICollection<Curso>? Cursos { get; set; }
    }
}
