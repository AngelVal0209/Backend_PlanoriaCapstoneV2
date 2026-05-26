using System.ComponentModel.DataAnnotations;

namespace PlanoriaCapstone.DTOs.Flashcard
{
    public class CrearFlashcardDTO
    {
        [Required]
        public int IdAnalisis { get; set; }

        [Required]
        [MaxLength(500)]
        public string Pregunta { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Respuesta { get; set; } = string.Empty;
    }
}