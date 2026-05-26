using Backend_PlanoriaCapstone.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal.Interface;
<<<<<<< HEAD
using PlanoriaCapstone.DTOs.Archivo;
using System.Security.Claims;
=======
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ArchivoController : ControllerBase
    {
        private readonly IArchivoService _archivoService;
        private readonly IArchivoRepository _archivoRepository;
        private readonly ILogger<ArchivoController> _logger;

        public ArchivoController(
            IArchivoService archivoService,
            IArchivoRepository archivoRepository,
            ILogger<ArchivoController> logger)
        {
            _archivoService = archivoService;
            _archivoRepository = archivoRepository;
            _logger = logger;
        }

        // GET api/archivo
        // Returns all files uploaded by the authenticated user
        [HttpGet]
        public async Task<IActionResult> ObtenerMisArchivos()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var archivos = await _archivoService.ObtenerArchivosUsuarioAsync(userId.Value);
            return Ok(archivos);
        }

        // GET api/archivo/{id}
        // Returns a specific file by ID (only if it belongs to the user)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var archivo = await _archivoRepository.ObtenerPorIdAsync(id);
            if (archivo == null) return NotFound("Archivo no encontrado.");
            if (archivo.IdUsuario != userId.Value) return Forbid();

            return Ok(archivo);
        }

        // POST api/archivo
        // Uploads a new file (.pdf or .txt) and triggers AI processing
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SubirArchivo( [FromForm] UploadArchivoDTO dto)
        {
<<<<<<< HEAD
            var userId = ObtenerUserId();
=======
            _logger.LogInformation("=== SubirArchivo INICIO: FileName={FileName}, Length={Length}", archivo?.FileName, archivo?.Length);

            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864

            if (userId == null)
                return Unauthorized("Token inválido.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.Archivo == null || dto.Archivo.Length == 0)
                return BadRequest("Archivo inválido.");

            const long maxSize = 10 * 1024 * 1024;

            if (dto.Archivo.Length > maxSize)
                return BadRequest("El archivo supera 10MB.");

            var extension =
                Path.GetExtension(dto.Archivo.FileName).ToLower();

<<<<<<< HEAD
=======
            if (archivo.Length > 10 * 1024 * 1024)
                return BadRequest("El archivo no puede superar los 10 MB.");

            var extension = Path.GetExtension(archivo.FileName).ToLower();
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
            if (extension != ".pdf" && extension != ".txt")
            {
                return BadRequest(
                    "Solo se permiten archivos .pdf o .txt.");
            }

            try
            {
<<<<<<< HEAD
                var nuevoArchivo =
                    await _archivoService.SubirArchivoAsync(
                        userId.Value,
                        dto.IdCurso,
                        dto.CantidadFlashcards,
                        dto.CantidadPreguntas,
                        dto.Archivo);

                return CreatedAtAction(
                    nameof(ObtenerPorId),
                    new { id = nuevoArchivo.IdArchivo },
                    nuevoArchivo);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    $"Error al procesar archivo: {ex.Message}");
=======
                _logger.LogInformation("Llamando a SubirArchivoAsync...");
                var nuevoArchivo = await _archivoService.SubirArchivoAsync(userId.Value, archivo);
                _logger.LogInformation("SubirArchivoAsync OK, Id={Id}", nuevoArchivo.IdArchivo);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoArchivo.IdArchivo }, nuevoArchivo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar archivo {FileName} del usuario {UserId}", archivo.FileName, userId);
                return StatusCode(500, "Error al procesar el archivo. Intente nuevamente más tarde.");
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
            }
        }

        // DELETE api/archivo/{id}
        // Deletes a file by ID (only if it belongs to the user)
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarArchivo(int id)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            try
            {
                var eliminado = await _archivoService.EliminarArchivoAsync(id, userId.Value);
                if (!eliminado) return NotFound("Archivo no encontrado.");
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}
