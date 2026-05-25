namespace PlanoriaCapstone.DTOs.Quiz
{
    public class ResolverQuizDTO
    {
        public int IdUsuario { get; set; }

        public int IdQuiz { get; set; }

        public int Correctas { get; set; }

        public int Incorrectas { get; set; }

        public decimal Puntaje { get; set; }

        public int TiempoMinutos { get; set; }

        public List<RespuestaPreguntaDTO> Respuestas
        { get; set; } = new();
    }

    public class RespuestaPreguntaDTO
    {
        public int IdPreguntaQuiz { get; set; }

        public string Respuesta { get; set; }
            = string.Empty;
    }
}