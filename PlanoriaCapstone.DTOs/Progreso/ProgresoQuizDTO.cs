namespace PlanoriaCapstone.DTOs.Progreso
{
    public class ProgresoQuizDTO
    {
        public int IdQuiz { get; set; }

        public decimal Puntaje { get; set; }

        public bool Completado { get; set; }

        public DateTime FechaRealizacion { get; set; }
    }
}