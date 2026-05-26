namespace PlanoriaCapstone.DTOs.Quiz
{
    public class PreguntaQuizDTO
    {
        public int IdPreguntaQuiz { get; set; }

        public string Pregunta { get; set; }
            = string.Empty;

        public string OpcionA { get; set; }
            = string.Empty;

        public string OpcionB { get; set; }
            = string.Empty;

        public string? OpcionC { get; set; }

        public string? OpcionD { get; set; }

        // Opcional:
        // SOLO si deseas mostrar respuestas correctas
        // después de resolver el quiz

        public string? RespuestaCorrecta { get; set; }

        public string? Explicacion { get; set; }
    }
}