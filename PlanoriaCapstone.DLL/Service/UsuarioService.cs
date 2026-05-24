using Microsoft.AspNetCore.Http;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal.Interface;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly IImageService _imageService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IImageService imageService)
        {
            _usuarioRepository = usuarioRepository;
            _imageService = imageService;
        }

        // =========================================
        // OBTENER USUARIO POR ID
        // =========================================

        public async Task<Usuario?> ObtenerPorIdAsync(
            int id)
        {
            return await _usuarioRepository
                .ObtenerPorIdAsync(id);
        }

        // =========================================
        // REGISTRAR USUARIO
        // =========================================

        public async Task<Usuario> RegistrarAsync(
            Usuario usuario,
            IFormFile? fotoPerfil)
        {
            // HASH PASSWORD
            if (!string.IsNullOrEmpty(usuario.PasswordHash))
            {
                usuario.PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(
                        usuario.PasswordHash);
            }

            // FOTO PERFIL
            if (fotoPerfil != null &&
                fotoPerfil.Length > 0)
            {
                var rutaImagen =
                    await _imageService
                        .GuardarImagenAsync(
                            fotoPerfil);

                usuario.FotoPerfilUrl =
                    rutaImagen;
            }

            usuario.FechaRegistro =
                DateTime.UtcNow;

            usuario.Estado = true;

            if (usuario.IdRol == 0)
            {
                usuario.IdRol = 2;
            }

            await _usuarioRepository
                .CrearAsync(usuario);

            await _usuarioRepository
                .GuardarCambiosAsync();

            return usuario;
        }

        // =========================================
        // EDITAR PERFIL
        // =========================================

        public async Task<bool> EditarPerfilAsync(
            int idUsuario,
            string nombre,
            string? apellido,
            string correo,
            IFormFile? fotoPerfil)
        {
            var usuario =
                await _usuarioRepository
                    .ObtenerPorIdAsync(
                        idUsuario);

            if (usuario == null)
                return false;

            // Check for duplicate email (excluding current user)
            var existente = await _usuarioRepository
                .ObtenerPorCorreoAsync(correo);

            if (existente != null && existente.IdUsuario != idUsuario)
                return false;

            usuario.Nombre = nombre;
            usuario.Apellido = apellido;
            usuario.Correo = correo;

            // FOTO
            if (fotoPerfil != null &&
                fotoPerfil.Length > 0)
            {
                var rutaImagen =
                    await _imageService
                        .GuardarImagenAsync(
                            fotoPerfil);

                usuario.FotoPerfilUrl =
                    rutaImagen;
            }

            await _usuarioRepository
                .ActualizarAsync(usuario);

            await _usuarioRepository
                .GuardarCambiosAsync();

            return true;
        }

        // =========================================
        // CAMBIAR PASSWORD
        // =========================================

        public async Task<bool> CambiarPasswordAsync(
            int idUsuario,
            string passwordActual,
            string nuevaPassword)
        {
            var usuario =
                await _usuarioRepository
                    .ObtenerPorIdAsync(
                        idUsuario);

            if (usuario == null)
                return false;

            var passwordValida =
                BCrypt.Net.BCrypt.Verify(
                    passwordActual,
                    usuario.PasswordHash);

            if (!passwordValida)
                return false;

            usuario.PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(
                    nuevaPassword);

            await _usuarioRepository
                .ActualizarAsync(usuario);

            await _usuarioRepository
                .GuardarCambiosAsync();

            return true;
        }

        // =========================================
        // ELIMINAR CUENTA
        // =========================================

        public async Task<bool> EliminarCuentaAsync(
            int idUsuario)
        {
            var usuario =
                await _usuarioRepository
                    .ObtenerPorIdAsync(
                        idUsuario);

            if (usuario == null)
                return false;

            await _usuarioRepository
                .EliminarAsync(usuario);

            await _usuarioRepository
                .GuardarCambiosAsync();

            return true;
        }
    }
}