namespace PlanoriaCapstone.DTOs.Quiz
{
    public class QuizResponseDTO
    {
        public int IdQuiz { get; set; }

<<<<<<< HEAD
        public string Titulo { get; set; }
            = string.Empty;
=======
        public int IdAnalisis { get; set; }

        public string Titulo { get; set; } = string.Empty;
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864

        public string? Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public List<PreguntaQuizDTO> Preguntas
        { get; set; } = new();
    }
}