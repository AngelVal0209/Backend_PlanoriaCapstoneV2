using PlanoriaCapstone.DTOs.Progreso;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IProgresoService
    {
        Task<ProgresoResponseDTO?>
            ObtenerProgresoAsync(
                int idUsuario,
                int idArchivo);

        Task<List<ProgresoResponseDTO>>
            ObtenerTodosUsuarioAsync(
                int idUsuario);

        Task<ProgresoResumenDTO>
            ObtenerResumenAsync(
                int idUsuario);

        Task<decimal>
            ObtenerPromedioQuizAsync(
                int idUsuario,
                int idArchivo);

        Task ActualizarProgresoAsync(
            int idUsuario,
            int idArchivo,
            int flashcardsCompletadas,
            int quizzesCompletados);
    }
}
