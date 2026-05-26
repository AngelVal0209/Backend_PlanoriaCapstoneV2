using PlanoriaCapstone.DTOs.Progreso;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IProgresoFlashcardService
    {
        // =====================================
        // MARCAR FLASHCARD COMPLETADA
        // =====================================

        Task RegistrarRespuestaAsync(
            int idUsuario,
            int idFlashcard,
            bool completado);

        // =====================================
        // OBTENER PROGRESO FLASHCARD
        // =====================================

        Task<ProgresoFlashcardDTO?>
            ObtenerAsync(
                int idUsuario,
                int idFlashcard);
    }
}