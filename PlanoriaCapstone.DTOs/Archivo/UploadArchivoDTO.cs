using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PlanoriaCapstone.DTOs.Archivo
{
    public class UploadArchivoDTO
    {
        [Required]
        public IFormFile Archivo { get; set; } = null!;

        public int? IdCurso { get; set; }

        [Range(1, 50)]
        public int CantidadFlashcards { get; set; } = 10;

        [Range(1, 30)]
        public int CantidadPreguntas { get; set; } = 5;
    }
}