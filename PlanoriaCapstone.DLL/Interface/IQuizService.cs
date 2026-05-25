using PlanoriaCapstone.DTOs.Quiz;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IQuizService
    {
        // =====================================
        // GET ALL QUIZZES
        // =====================================

        Task<IEnumerable<QuizResumenDTO>>
            ObtenerTodosAsync();

        // =====================================
        // GET QUIZZES BY ARCHIVO
        // =====================================

        Task<IEnumerable<QuizResponseDTO>>
            ObtenerPorArchivoAsync(int idArchivo);

        // =====================================
        // GET QUIZ BY ID
        // =====================================

        Task<QuizResponseDTO?>
            ObtenerPorIdAsync(int idQuiz);

        // =====================================
        // RESOLVER QUIZ
        // =====================================

        Task<bool> ResolverQuizAsync(
            int idUsuario,
            int idQuiz,
            int correctas,
            int incorrectas,
            decimal puntaje,
            int tiempoMinutos);
    }
}