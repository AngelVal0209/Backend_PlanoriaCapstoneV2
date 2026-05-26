using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Bll.Service
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerarToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.IdRol == 1 ? "ADMIN" : "USER")
            };

            var jwtKey = _config["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key no está configurado.");
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireMinutes = Convert.ToInt32(_config["Jwt:ExpireMinutes"] ?? "60");
            if (expireMinutes <= 0) expireMinutes = 60;

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"] ?? "PlanoriaAPI",
                audience: _config["Jwt:Audience"] ?? "PlanoriaClient",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
