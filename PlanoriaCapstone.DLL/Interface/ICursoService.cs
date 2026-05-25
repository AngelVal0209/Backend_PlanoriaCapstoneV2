using PlanoriaCapstone.DTOs.Curso;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface ICursoService
    {
        // =====================================
        // CREAR CURSO
        // =====================================

        Task<CursoResponseDTO>
            CrearAsync(
                CrearCursoDTO dto, int idUsuario);

        // =====================================
        // OBTENER TODOS
        // =====================================

        Task<IEnumerable<CursoResponseDTO>>
            ObtenerTodosAsync();

        // =====================================
        // OBTENER POR ID
        // =====================================

        Task<CursoResponseDTO?>
            ObtenerPorIdAsync(
                int idCurso);

        // =====================================
        // ELIMINAR
        // =====================================

        Task<bool>
            EliminarAsync(
                int idCurso);
    }
}