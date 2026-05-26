using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal.Interface;
using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Bll.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtService _jwtService;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            IJwtService jwtService)
        {
            _usuarioRepository = usuarioRepository;
            _jwtService = jwtService;
        }

        public async Task<string> LoginAsync(string correo, string password)
        {
            var usuario = await _usuarioRepository
                .ObtenerPorCorreoAsync(correo);

            if (usuario == null || !usuario.Estado)
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            if (usuario.Provider == "GOOGLE")
                throw new InvalidOperationException("Esta cuenta usa Google. Inicia sesión con Google.");

            var passwordValida = BCrypt.Net.BCrypt.Verify(
                password,
                usuario.PasswordHash
            );

            if (!passwordValida)
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            var token = _jwtService.GenerarToken(usuario);

            return token;
        }
    }
}
