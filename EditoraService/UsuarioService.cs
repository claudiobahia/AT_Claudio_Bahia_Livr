using Editora.Domain;
using Editora.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Editora.Service
{
    public class UsuarioService
    {
        private EditoraContext Context { get; set; }
        private IConfiguration Configuration { get; }

        public UsuarioService(EditoraContext editoraContext, IConfiguration configuration)
        {
            this.Context = editoraContext;
            this.Configuration = configuration;
        }

        public string Login(string login, string senha)
        {
            var usuario = Context.Usuarios.Where(x => x.Login == login && x.Password == senha).FirstOrDefault();
            if (usuario == null)
                return null;
            return CreateToken(usuario);
        }

        private string CreateToken(Usuario usuario)
        {
            var key = Encoding.UTF8.GetBytes(this.Configuration["Token:Secret"]);

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, usuario.Login));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Audience = "EDITORA-API",
                Issuer = "EDITORA-API"
            };

            var secutiryToken = tokenHandler.CreateToken(tokenDescription);

            var token = tokenHandler.WriteToken(secutiryToken);

            return token;
        }
    }
}
