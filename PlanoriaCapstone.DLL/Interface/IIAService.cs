using PlanoriaCapstone.DTOs.Archivo;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IIAService
    {
        Task<AnalisisDocumentoDto> AnalizarTextoAsync(
            string texto,
            int cantidadFlashcards,
            int cantidadPreguntas);
    }
}