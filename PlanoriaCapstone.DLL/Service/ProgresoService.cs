using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.DTOs.Progreso;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service
{
    public class ProgresoService
         : IProgresoService
    {
        private readonly AppDbContext _context;

        public ProgresoService(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProgresoResponseDTO?>
            ObtenerProgresoAsync(
                int idUsuario,
                int idArchivo)
        {
            var progreso = await _context
                .ProgresoArchivos
                .Where(p =>
                    p.IdUsuario == idUsuario &&
                    p.IdArchivo == idArchivo)
                .Select(p =>
                    new ProgresoResponseDTO
                    {
                        IdProgresoArchivo =
                            p.IdProgresoArchivo,
                        IdUsuario = p.IdUsuario,
                        IdArchivo = p.IdArchivo,
                        FlashcardsCompletadas =
                            p.FlashcardsCompletadas,
                        FlashcardsTotales =
                            p.FlashcardsTotales,
                        QuizzesCompletados =
                            p.QuizzesCompletados,
                        QuizzesTotales =
                            p.QuizzesTotales,
                        PorcentajeProgreso =
                            p.PorcentajeProgreso,
                        Completado = p.Completado,
                        UltimaSesion = p.UltimaSesion
                    })
                .FirstOrDefaultAsync();

            if (progreso != null)
            {
                progreso.PromedioPuntaje =
                    await _context.HistorialQuizzes
                        .Where(h =>
                            h.IdUsuario == idUsuario
                            && h.Quiz!.AnalisisIA!
                                .IdArchivo == idArchivo)
                        .AverageAsync(h =>
                            (decimal?)h.Puntaje)
                        ?? 0;
                return progreso;
            }

            // AUTO-CREATE if not exists
            var totalFlashcards =
                await _context.Flashcards
                    .CountAsync(f =>
                        f.AnalisisIA!.IdArchivo
                            == idArchivo);

            var totalQuizzes =
                await _context.Quizzes
                    .CountAsync(q =>
                        q.AnalisisIA!.IdArchivo
                            == idArchivo);

            var nuevo = new ProgresoArchivo
            {
                IdUsuario = idUsuario,
                IdArchivo = idArchivo,
                FlashcardsCompletadas = 0,
                FlashcardsTotales = totalFlashcards,
                QuizzesCompletados = 0,
                QuizzesTotales = totalQuizzes,
                PorcentajeProgreso = 0,
                Completado = false,
                UltimaSesion = DateTime.UtcNow
            };

            _context.ProgresoArchivos.Add(nuevo);
            await _context.SaveChangesAsync();

            return new ProgresoResponseDTO
            {
                IdProgresoArchivo =
                    nuevo.IdProgresoArchivo,
                IdUsuario = nuevo.IdUsuario,
                IdArchivo = nuevo.IdArchivo,
                FlashcardsCompletadas =
                    nuevo.FlashcardsCompletadas,
                FlashcardsTotales =
                    nuevo.FlashcardsTotales,
                QuizzesCompletados =
                    nuevo.QuizzesCompletados,
                QuizzesTotales = nuevo.QuizzesTotales,
                PorcentajeProgreso =
                    nuevo.PorcentajeProgreso,
                PromedioPuntaje = 0,
                Completado = nuevo.Completado,
                UltimaSesion = nuevo.UltimaSesion
            };
        }

        public async Task<List<ProgresoResponseDTO>>
            ObtenerTodosUsuarioAsync(
                int idUsuario)
        {
            var progresos = await _context
                .ProgresoArchivos
                .Where(p =>
                    p.IdUsuario == idUsuario)
                .Select(p =>
                    new ProgresoResponseDTO
                    {
                        IdProgresoArchivo =
                            p.IdProgresoArchivo,
                        IdUsuario = p.IdUsuario,
                        IdArchivo = p.IdArchivo,
                        FlashcardsCompletadas =
                            p.FlashcardsCompletadas,
                        FlashcardsTotales =
                            p.FlashcardsTotales,
                        QuizzesCompletados =
                            p.QuizzesCompletados,
                        QuizzesTotales =
                            p.QuizzesTotales,
                        PorcentajeProgreso =
                            p.PorcentajeProgreso,
                        Completado = p.Completado,
                        UltimaSesion = p.UltimaSesion
                    })
                .ToListAsync();

            foreach (var p in progresos)
            {
                p.PromedioPuntaje =
                    await _context.HistorialQuizzes
                        .Where(h =>
                            h.IdUsuario == idUsuario
                            && h.Quiz!.AnalisisIA!
                                .IdArchivo
                                == p.IdArchivo)
                        .AverageAsync(h =>
                            (decimal?)h.Puntaje)
                        ?? 0;
            }

            return progresos;
        }

        public async Task<ProgresoResumenDTO>
            ObtenerResumenAsync(
                int idUsuario)
        {
            var progresos = await _context
                .ProgresoArchivos
                .Where(p =>
                    p.IdUsuario == idUsuario)
                .ToListAsync();

            var idsArchivo = progresos
                .Select(p => p.IdArchivo)
                .ToList();

            var promedioGeneral =
                await _context.HistorialQuizzes
                    .Where(h =>
                        h.IdUsuario == idUsuario
                        && idsArchivo.Contains(
                            h.Quiz!.AnalisisIA!
                                .IdArchivo))
                    .AverageAsync(h =>
                        (decimal?)h.Puntaje)
                    ?? 0;

            return new ProgresoResumenDTO
            {
                TotalArchivos = progresos.Count,
                TotalCompletados = progresos
                    .Count(p => p.Completado),
                PromedioGeneral =
                    Math.Round(promedioGeneral, 2)
            };
        }

        public async Task<decimal>
        ObtenerPromedioQuizAsync(
         int idUsuario,
         int idArchivo)
        {
            return await _context.HistorialQuizzes
                .Where(h =>
                    h.IdUsuario == idUsuario
                    && h.Quiz!.AnalisisIA!
                        .IdArchivo == idArchivo)
                .AverageAsync(h =>
                    (decimal?)h.Puntaje)
                ?? 0;
        }

        public async Task ActualizarProgresoAsync(
            int idUsuario,
            int idArchivo,
            int flashcardsCompletadas,
            int quizzesCompletados)
        {
            var progreso = await _context
                .ProgresoArchivos
                .FirstOrDefaultAsync(p =>
                    p.IdUsuario == idUsuario &&
                    p.IdArchivo == idArchivo);

            if (progreso == null)
            {
                var totalFlashcards =
                    await _context.Flashcards
                        .CountAsync(f =>
                            f.AnalisisIA!.IdArchivo
                                == idArchivo);

                var totalQuizzes =
                    await _context.Quizzes
                        .CountAsync(q =>
                            q.AnalisisIA!.IdArchivo
                                == idArchivo);

                progreso = new ProgresoArchivo
                {
                    IdUsuario = idUsuario,
                    IdArchivo = idArchivo,
                    FlashcardsCompletadas =
                        flashcardsCompletadas,
                    FlashcardsTotales =
                        totalFlashcards,
                    QuizzesCompletados =
                        quizzesCompletados,
                    QuizzesTotales = totalQuizzes,
                    UltimaSesion = DateTime.UtcNow
                };

                progreso.PorcentajeProgreso =
                    CalcularPorcentaje(progreso);
                progreso.Completado =
                    progreso.PorcentajeProgreso
                    >= 100;

                _context.ProgresoArchivos
                    .Add(progreso);
            }
            else
            {
                progreso.FlashcardsCompletadas =
                    flashcardsCompletadas;
                progreso.QuizzesCompletados =
                    quizzesCompletados;
                progreso.UltimaSesion =
                    DateTime.UtcNow;
                progreso.PorcentajeProgreso =
                    CalcularPorcentaje(progreso);
                progreso.Completado =
                    progreso.PorcentajeProgreso
                    >= 100;
            }

            await _context.SaveChangesAsync();
        }

        private decimal CalcularPorcentaje(
            ProgresoArchivo progreso)
        {
            decimal total =
                progreso.FlashcardsTotales +
                progreso.QuizzesTotales;

            decimal completado =
                progreso.FlashcardsCompletadas +
                progreso.QuizzesCompletados;

            if (total == 0)
                return 0;

            return Math.Round(
                (completado / total) * 100,
                2);
        }
    }
}
