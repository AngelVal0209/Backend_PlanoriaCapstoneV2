using PlanoriaCapstone.DTOs.Quiz;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IQuizService
    {
        Task<List<QuizResponseDTO>> ObtenerPorArchivoAsync(
            int idArchivo);

        Task<QuizResponseDTO?> ObtenerPorIdAsync(
            int idQuiz);

        Task<bool> ResolverQuizAsync(
            int idUsuario,
            int idQuiz,
            int correctas,
            int incorrectas,
            decimal puntaje,
            int tiempoResolucionMinutos);

        Task<bool> VerificarAccesoArchivoAsync(
            int idArchivo,
            int idUsuario);

        Task<bool> VerificarAccesoQuizAsync(
            int idQuiz,
            int idUsuario);
    }
}
