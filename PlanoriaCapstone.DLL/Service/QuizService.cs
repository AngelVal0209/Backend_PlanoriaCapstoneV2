using PlanoriaCapstone.Bll.Interface;
using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Bll.Service
{
    public class QuizService : IQuizService
    {
        private readonly AppDbContext _context;
        private readonly IProgresoService _progresoService;

        public QuizService(
            AppDbContext context,
            IProgresoService progresoService)
        {
            _context = context;
            _progresoService = progresoService;
        }

        public async Task<IEnumerable<Quiz>> ObtenerPorArchivoAsync(
            int idArchivo)
        {
            return await _context.Quizzes
                .Include(q => q.PreguntasQuiz)
                .Include(q => q.AnalisisIA)
                .Where(q =>
                    q.AnalisisIA != null &&
                    q.AnalisisIA.IdArchivo == idArchivo)
                .ToListAsync();
        }

        public async Task<Quiz?> ObtenerPorIdAsync(
            int idQuiz)
        {
            return await _context.Quizzes
                .Include(q => q.PreguntasQuiz)
                .FirstOrDefaultAsync(q =>
                    q.IdQuiz == idQuiz);
        }

        public async Task<bool> ResolverQuizAsync(
            int idUsuario,
            int idQuiz,
            int correctas,
            int incorrectas,
            decimal puntaje,
            int tiempoResolucionMinutos)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.AnalisisIA)
                .FirstOrDefaultAsync(q =>
                    q.IdQuiz == idQuiz);

            if (quiz == null)
                return false;

            var historial = new HistorialQuiz
            {
                IdUsuario = idUsuario,
                IdQuiz = idQuiz,
                Puntaje = puntaje,
                CantidadCorrectas = correctas,
                CantidadIncorrectas = incorrectas,
                TiempoResolucionMinutos =
                    tiempoResolucionMinutos,
                FechaRealizacion = DateTime.UtcNow
            };

            _context.HistorialQuizzes.Add(historial);

            await _context.SaveChangesAsync();

            // ACTUALIZAR PROGRESO
            if (quiz.AnalisisIA != null)
            {
                var quizzesCompletados =
                    await _context.HistorialQuizzes
                        .CountAsync(hq =>
                            hq.IdUsuario == idUsuario &&
                            hq.Quiz != null &&
                            hq.Quiz.IdAnalisis == quiz.IdAnalisis);

                await _progresoService
                    .ActualizarProgresoAsync(
                        idUsuario,
                        quiz.AnalisisIA.IdArchivo,
                        0,
                        quizzesCompletados);
            }

            return true;
        }
    }
}
