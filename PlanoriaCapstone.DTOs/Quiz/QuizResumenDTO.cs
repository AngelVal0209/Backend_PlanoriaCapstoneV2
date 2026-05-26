namespace PlanoriaCapstone.DTOs.Quiz
{
    public class QuizResumenDTO
    {
        public int IdQuiz { get; set; }

        public string Titulo { get; set; }
            = string.Empty;

        public string? Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int TotalPreguntas { get; set; }
    }
}