using Backend_PlanoriaCapstone.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal.Interface;
using PlanoriaCapstone.DTOs.Auth;
using PlanoriaCapstone.DTOs.Response;
using PlanoriaCapstone.Models;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtService _jwtService;
        private readonly IUsuarioService _usuarioService;

        public AuthController(
            IAuthService authService,
            IUsuarioRepository usuarioRepository,
            IJwtService jwtService,
            IUsuarioService usuarioService)
        {
            _authService = authService;
            _usuarioRepository = usuarioRepository;
            _jwtService = jwtService;
            _usuarioService = usuarioService;
        }

        // POST api/auth/login
        // Authenticates an existing user and returns a JWT token
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuario = await _usuarioRepository.ObtenerPorCorreoAsync(dto.Correo);

            if (usuario == null || !usuario.Estado)
                return Unauthorized("Credenciales inválidas.");

            if (usuario.Provider == "GOOGLE")
                return BadRequest("Esta cuenta usa Google. Por favor inicia sesión con Google.");

            var passwordValida = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash);

            if (!passwordValida)
                return Unauthorized("Credenciales inválidas.");

            var token = _jwtService.GenerarToken(usuario);

            return Ok(new AuthResponseDTO
            {
                Token     = token,
                IdUsuario = usuario.IdUsuario,
                Nombre    = usuario.Nombre,
                Correo    = usuario.Correo,
                Rol       = usuario.IdRol == 1 ? "ADMIN" : "USER"
            });
        }

        // POST api/auth/register
        // Registers a new user account
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check for duplicate email
            var existente = await _usuarioRepository
                .ObtenerPorCorreoAsync(dto.Correo);

            if (existente != null)
                return Conflict("Ya existe una cuenta con ese correo electrónico.");

            var nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,

                // RAW password (will be hashed by UsuarioService.RegistrarAsync)
                PasswordHash = dto.Password
                    ?? throw new ArgumentException("La contraseña es requerida."),

                Provider = "LOCAL",
                Estado = true,
                FechaRegistro = DateTime.UtcNow,
                IdRol = 2
            };

            // null porque ya no usamos multipart/form-data
            var creado = await _usuarioService
                .RegistrarAsync(nuevoUsuario, null);

            var token = _jwtService.GenerarToken(creado);

            return Ok(new AuthResponseDTO
            {
                Token = token,
                IdUsuario = creado.IdUsuario,
                Nombre = creado.Nombre,
                Correo = creado.Correo,
                Rol = "USER"
            });
        }

        // GET api/auth/me
        // Returns the profile of the currently authenticated user
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();

            var usuario = await _usuarioRepository.ObtenerPorIdAsync(userId.Value);
            if (usuario == null) return NotFound();

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
    }
}
