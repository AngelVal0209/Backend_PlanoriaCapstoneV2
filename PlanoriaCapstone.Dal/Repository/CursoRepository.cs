using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Dal.Interface;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal.Repository
{
    public class CursoRepository
        : ICursoRepository
    {
        private readonly AppDbContext _context;

        public CursoRepository(
            AppDbContext context)
        {
            _context = context;
        }

        // =====================================
        // CREAR
        // =====================================

        public async Task CrearAsync(
            Curso curso)
        {
            await _context.Cursos
                .AddAsync(curso);
        }

        // =====================================
        // GET ALL
        // =====================================

        public async Task<IEnumerable<Curso>>
            ObtenerTodosAsync()
        {
            return await _context.Cursos 
                .Include(c => c.Archivos)
                .ToListAsync();
        }

        // =====================================
        // GET BY ID
        // =====================================

        public async Task<Curso?>
            ObtenerPorIdAsync(
                int idCurso)
        {
            return await _context.Cursos
                .Include(c => c.Archivos)
                .FirstOrDefaultAsync(c =>c.IdCurso == idCurso);
        }

        // =====================================
        // DELETE
        // =====================================

        public Task EliminarAsync(
            Curso curso)
        {
            _context.Cursos.Remove(curso);

            return Task.CompletedTask;
        }

        // =====================================
        // SAVE CHANGES
        // =====================================

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}