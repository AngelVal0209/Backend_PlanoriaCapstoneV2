using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.DTOs.Quiz;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service
{
    public class QuizService : IQuizService
    {
        private readonly AppDbContext _context;

        public QuizService(AppDbContext context)
        {
            _context = context;
        }

<<<<<<< HEAD
        // =====================================
        // GET ALL QUIZZES
        // =====================================

        public async Task<IEnumerable<QuizResumenDTO>>
            ObtenerTodosAsync()
        {
            return await _context.Quizzes
                .Select(q => new QuizResumenDTO
                {
                    IdQuiz = q.IdQuiz,

                    Titulo = q.Titulo,

                    Descripcion = q.Descripcion,

                    FechaCreacion =
                        q.FechaCreacion,

                    TotalPreguntas =
                        q.PreguntasQuiz.Count()
=======
        public async Task<List<QuizResponseDTO>> ObtenerPorArchivoAsync(
            int idArchivo)
        {
            return await _context.Quizzes
                .Where(q =>
                    q.AnalisisIA != null &&
                    q.AnalisisIA.IdArchivo == idArchivo)
                .Select(q => new QuizResponseDTO
                {
                    IdQuiz = q.IdQuiz,
                    Titulo = q.Titulo,
                    Descripcion = q.Descripcion,
                    FechaCreacion = q.FechaCreacion,
                    IdAnalisis = q.IdAnalisis,
                    Preguntas = q.PreguntasQuiz!
                        .Select(p => new PreguntaQuizDTO
                        {
                            IdPreguntaQuiz =
                                p.IdPreguntaQuiz,
                            Pregunta = p.Pregunta,
                            OpcionA = p.OpcionA,
                            OpcionB = p.OpcionB,
                            OpcionC = p.OpcionC,
                            OpcionD = p.OpcionD
                        })
                        .ToList()
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
                })
                .ToListAsync();
        }

<<<<<<< HEAD
        // =====================================
        // GET QUIZZES BY ARCHIVO
        // =====================================

        public async Task<IEnumerable<QuizResponseDTO>>
            ObtenerPorArchivoAsync(int idArchivo)
        {
            return await _context.Quizzes
                .Where(q =>
                    q.AnalisisIA.IdArchivo
                    == idArchivo)

                .Select(q => new QuizResponseDTO
                {
                    IdQuiz = q.IdQuiz,

                    Titulo = q.Titulo,

                    Descripcion =
                        q.Descripcion,

                    FechaCreacion =
                        q.FechaCreacion,

                    Preguntas =
                        q.PreguntasQuiz
                        .Select(p =>
                            new PreguntaQuizDTO
                            {
                                IdPreguntaQuiz =
                                    p.IdPreguntaQuiz,

                                Pregunta =
                                    p.Pregunta,

                                OpcionA =
                                    p.OpcionA,

                                OpcionB =
                                    p.OpcionB,

                                OpcionC =
                                    p.OpcionC,

                                OpcionD =
                                    p.OpcionD
                            })
                        .ToList()
                })
                .ToListAsync();
=======
        public async Task<QuizResponseDTO?> ObtenerPorIdAsync(
            int idQuiz)
        {
            return await _context.Quizzes
                .Where(q => q.IdQuiz == idQuiz)
                .Select(q => new QuizResponseDTO
                {
                    IdQuiz = q.IdQuiz,
                    Titulo = q.Titulo,
                    Descripcion = q.Descripcion,
                    FechaCreacion = q.FechaCreacion,
                    IdAnalisis = q.IdAnalisis,
                    Preguntas = q.PreguntasQuiz!
                        .Select(p => new PreguntaQuizDTO
                        {
                            IdPreguntaQuiz =
                                p.IdPreguntaQuiz,
                            Pregunta = p.Pregunta,
                            OpcionA = p.OpcionA,
                            OpcionB = p.OpcionB,
                            OpcionC = p.OpcionC,
                            OpcionD = p.OpcionD
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
        }

        // =====================================
        // GET QUIZ BY ID
        // =====================================

        public async Task<QuizResponseDTO?>
            ObtenerPorIdAsync(int idQuiz)
        {
            return await _context.Quizzes
                .Where(q =>
                    q.IdQuiz == idQuiz)

                .Select(q => new QuizResponseDTO
                {
                    IdQuiz = q.IdQuiz,

                    Titulo = q.Titulo,

                    Descripcion =
                        q.Descripcion,

                    FechaCreacion =
                        q.FechaCreacion,

                    Preguntas =
                        q.PreguntasQuiz
                        .Select(p =>
                            new PreguntaQuizDTO
                            {
                                IdPreguntaQuiz =
                                    p.IdPreguntaQuiz,

                                Pregunta =
                                    p.Pregunta,

                                OpcionA =
                                    p.OpcionA,

                                OpcionB =
                                    p.OpcionB,

                                OpcionC =
                                    p.OpcionC,

                                OpcionD =
                                    p.OpcionD,

                                RespuestaCorrecta =
                                    p.RespuestaCorrecta,

                                Explicacion =
                                    p.Explicacion
                            })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        // =====================================
        // RESOLVER QUIZ
        // =====================================

        public async Task<bool>
            ResolverQuizAsync(
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

                CantidadCorrectas =
                    correctas,

                CantidadIncorrectas =
                    incorrectas,

                TiempoResolucionMinutos =
                    tiempoResolucionMinutos,

                FechaRealizacion =
                    DateTime.UtcNow
            };

            _context.HistorialQuizzes
                .Add(historial);

            // =====================================
            // GUARDAR PROGRESO QUIZ
            // =====================================

            var progresoQuiz = new ProgresoQuiz
            {
                IdUsuario = idUsuario,

                IdQuiz = idQuiz,

                Puntaje = puntaje,

                Completado = true,

                FechaRealizacion = DateTime.UtcNow
            };

            _context.ProgresoQuizzes
                .Add(progresoQuiz);

            await _context.SaveChangesAsync();

<<<<<<< HEAD
=======
            if (quiz.AnalisisIA != null)
            {
                var quizzesCompletados =
                    await _context.HistorialQuizzes
                        .Where(hq =>
                            hq.IdUsuario == idUsuario &&
                            hq.Quiz != null &&
                            hq.Quiz.IdAnalisis ==
                                quiz.IdAnalisis)
                        .Select(hq => hq.IdQuiz)
                        .Distinct()
                        .CountAsync();

                await _progresoService
                    .ActualizarProgresoAsync(
                        idUsuario,
                        quiz.AnalisisIA.IdArchivo,
                        0,
                        quizzesCompletados);
            }

>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
            return true;
        }

        public async Task<bool> VerificarAccesoArchivoAsync(
            int idArchivo,
            int idUsuario)
        {
            return await _context.ArchivosSubidos
                .AnyAsync(a =>
                    a.IdArchivo == idArchivo
                    && a.IdUsuario == idUsuario);
        }

        public async Task<bool> VerificarAccesoQuizAsync(
            int idQuiz,
            int idUsuario)
        {
            return await _context.Quizzes
                .AnyAsync(q =>
                    q.IdQuiz == idQuiz
                    && q.AnalisisIA != null
                    && q.AnalisisIA.ArchivoSubido != null
                    && q.AnalisisIA.ArchivoSubido.IdUsuario == idUsuario);
        }
    }
}