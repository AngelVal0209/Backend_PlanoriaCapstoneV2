using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.Dal.Interface;
using PlanoriaCapstone.DTOs.Curso;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _repository;
        private readonly AppDbContext _context;

        public CursoService(
            ICursoRepository repository,
            AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        // =====================================
        // CREAR CURSO
        // =====================================

        public async Task<CursoResponseDTO> CrearAsync(
        CrearCursoDTO dto, int idUsuario)
        {
            var curso = new Curso
            {
                IdUsuario = idUsuario,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                FechaCreacion = DateTime.UtcNow
            };

            await _repository.CrearAsync(curso);

            await _repository.GuardarCambiosAsync();

            // ASOCIAR ARCHIVOS AL CURSO
            var archivos = await _context.ArchivosSubidos
                .Where(a => dto.IdArchivos.Contains(a.IdArchivo))
                .ToListAsync();

            foreach (var archivo in archivos)
            {
                archivo.IdCursos = curso.IdCurso;
            }

            await _repository.GuardarCambiosAsync();

            return new CursoResponseDTO
            {
                IdCurso = curso.IdCurso,
                Nombre = curso.Nombre,
                Descripcion = curso.Descripcion,
                FechaCreacion = curso.FechaCreacion,

                Archivos = archivos.Select(a =>
                    new ArchivoCursoDTO
                    {
                        IdArchivo = a.IdArchivo,
                        NombreArchivo = a.NombreArchivo,
                        UrlArchivo = a.UrlArchivo
                    }).ToList()
            };
        }

        // =====================================
        // GET ALL CURSOS
        // =====================================

        public async Task<IEnumerable<CursoResponseDTO>>
            ObtenerTodosAsync()
        {
            var cursos =
                await _repository
                    .ObtenerTodosAsync();

            return cursos.Select(c => new CursoResponseDTO
            {
                IdCurso = c.IdCurso,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                FechaCreacion = c.FechaCreacion,
                
                Archivos = c.Archivos?
                .Select(a => new ArchivoCursoDTO
                {
                    IdArchivo = a.IdArchivo,
                    NombreArchivo = a.NombreArchivo,
                    UrlArchivo = a.UrlArchivo
                }).ToList()
                ?? new List<ArchivoCursoDTO>()
            });
        }

        // =====================================
        // GET CURSO BY ID
        // =====================================

        public async Task<CursoResponseDTO?>
            ObtenerPorIdAsync(
                int idCurso)
        {
            var curso =
                await _repository
                    .ObtenerPorIdAsync(idCurso);

            if (curso == null)
                return null;

            return new CursoResponseDTO
            {
                IdCurso = curso.IdCurso,
                Nombre = curso.Nombre,
                Descripcion = curso.Descripcion,
                FechaCreacion = curso.FechaCreacion,

                Archivos = curso.Archivos?
                .Select(a => new ArchivoCursoDTO
                {
                    IdArchivo = a.IdArchivo,
                    NombreArchivo = a.NombreArchivo,
                    UrlArchivo = a.UrlArchivo
                }).ToList()
                ?? new List<ArchivoCursoDTO>()
            };
        }

        // =====================================
        // DELETE CURSO
        // =====================================

        public async Task<bool>
            EliminarAsync(
                int idCurso)
        {
            var curso =
                await _repository
                    .ObtenerPorIdAsync(idCurso);

            if (curso == null)
                return false;

            await _repository
                .EliminarAsync(curso);

            await _repository
                .GuardarCambiosAsync();

            return true;
        }
    }
}