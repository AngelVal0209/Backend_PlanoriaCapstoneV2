using Backend_PlanoriaCapstone.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Auth;
using PlanoriaCapstone.DTOs.Response;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET api/usuario/perfil
        // Returns the profile of the currently authenticated user
        [HttpGet("perfil")]
        public async Task<IActionResult> ObtenerPerfil()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var usuario = await _usuarioService.ObtenerPorIdAsync(userId.Value);
            if (usuario == null) return NotFound("Usuario no encontrado.");

            return Ok(new UsuarioResponseDTO
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                FotoPerfilUrl = usuario.FotoPerfilUrl,
                Provider = usuario.Provider ?? "LOCAL",
                RachaDias = usuario.RachaDias,
                Puntos = usuario.Puntos,
                Nivel = usuario.Nivel
            });
        }

        // PUT api/usuario/perfil
        // Updates the authenticated user's profile (multipart for optional photo)
        [HttpPut("perfil")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditarPerfil([FromForm] EditUsuarioDTO dto, IFormFile? fotoPerfil)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var actualizado = await _usuarioService.EditarPerfilAsync(
                userId.Value,
                dto.Nombre,
                dto.Apellido,
                dto.Correo,
                fotoPerfil);

            if (!actualizado)
                return BadRequest("No se pudo actualizar el perfil.");

            return Ok(new { success = true, mensaje = "Perfil actualizado correctamente." });
        }

        // PUT api/usuario/password
        // Changes the authenticated user's password
        [HttpPut("password")]
        public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var cambiado = await _usuarioService.CambiarPasswordAsync(
                userId.Value,
                dto.PasswordActual,
                dto.NuevaPassword);

            if (!cambiado)
                return BadRequest("La contraseña actual es incorrecta.");

            return Ok(new { success = true, mensaje = "Contraseña actualizada correctamente." });
        }

        // DELETE api/usuario
        // Permanently deletes the authenticated user's account
        [HttpDelete]
        public async Task<IActionResult> EliminarCuenta()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var eliminado = await _usuarioService.EliminarCuentaAsync(userId.Value);
            if (!eliminado)
                return BadRequest("No se pudo eliminar la cuenta.");

            return NoContent();
        }


    }
}
