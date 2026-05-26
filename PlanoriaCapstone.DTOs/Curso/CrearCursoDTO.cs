namespace PlanoriaCapstone.DTOs.Curso
{
    public class CrearCursoDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public List<int> IdArchivos { get; set; } = new();
    }
}