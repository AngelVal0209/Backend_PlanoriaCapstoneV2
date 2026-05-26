using PlanoriaCapstone.DTOs.Progreso;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IProgresoQuizService
    {
        // =====================================
        // GUARDAR PROGRESO QUIZ
        // =====================================

        Task RegistrarResultadoAsync(
            int idUsuario,
            int idQuiz,
            decimal puntaje);

        // =====================================
        // OBTENER PROGRESO QUIZ
        // =====================================

        Task<ProgresoQuizDTO?>
            ObtenerAsync(
                int idUsuario,
                int idQuiz);
    }
}