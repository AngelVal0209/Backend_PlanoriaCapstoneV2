using PlanoriaCapstone.DTOs.Flashcard;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IFlashcardService
    {
        Task<List<FlashcardResponseDTO>>
            ObtenerPorArchivoAsync(
                int idAnalisis);

        Task<bool>
            ResponderAsync(
                int idUsuario,
                int idFlashcard,
                bool correcta,
                int tiempoRespuestaSegundos);

        Task<FlashcardResponseDTO?>
            ObtenerPorIdAsync(
                int idFlashcard);
    }
}
