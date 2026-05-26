using PlanoriaCapstone.DTOs.Flashcard;
<<<<<<< HEAD
using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864

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

<<<<<<< HEAD
        Task<Flashcard?> 
            ObtenerPorIdAsync(int idFlashcard);

        Task<FlashcardResponseDTO> CrearManualAsync(CrearFlashcardDTO dto);

        Task<IEnumerable<FlashcardResponseDTO>> ObtenerTodosAsync();
=======
        Task<FlashcardResponseDTO?>
            ObtenerPorIdAsync(
                int idFlashcard);

        Task<bool> VerificarAccesoAnalisisAsync(
            int idAnalisis,
            int idUsuario);

        Task<bool> VerificarAccesoFlashcardAsync(
            int idFlashcard,
            int idUsuario);
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
    }
}
