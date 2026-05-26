using PlanoriaCapstone.DTOs.Quiz;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IQuizService
    {
<<<<<<< HEAD
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
=======
        Task<List<QuizResponseDTO>> ObtenerPorArchivoAsync(
            int idArchivo);

        Task<QuizResponseDTO?> ObtenerPorIdAsync(
            int idQuiz);
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864

        Task<bool> ResolverQuizAsync(
            int idUsuario,
            int idQuiz,
            int correctas,
            int incorrectas,
            decimal puntaje,
<<<<<<< HEAD
            int tiempoMinutos);
=======
            int tiempoResolucionMinutos);

        Task<bool> VerificarAccesoArchivoAsync(
            int idArchivo,
            int idUsuario);

        Task<bool> VerificarAccesoQuizAsync(
            int idQuiz,
            int idUsuario);
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
    }
}