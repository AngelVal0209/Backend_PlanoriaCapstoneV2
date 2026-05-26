namespace PlanoriaCapstone.DTOs.Curso
{
    public class CursoResponseDTO
    {
        public int IdCurso { get; set; }

        public string Nombre { get; set; }
            = string.Empty;

        public string? Descripcion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public List<ArchivoCursoDTO> Archivos { get; set; }
            = new();
    }

    public class ArchivoCursoDTO
    {
        public int IdArchivo { get; set; }

        public string NombreArchivo { get; set; }
            = string.Empty;

        public string UrlArchivo { get; set; }
            = string.Empty;
    }
}