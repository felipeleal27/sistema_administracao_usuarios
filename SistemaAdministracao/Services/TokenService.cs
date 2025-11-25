using SistemaAdministracao.Models;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SistemaAdministracao.Configs;
using System.Security.Claims;

namespace SistemaAdministracao.Services
{
    public class TokenService
    {
        public string Generate(Usuario user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(JWTConfiguration.PrivateKey);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddMinutes(1),
            };
                
       

            var token = handler.CreateToken(tokenDescriptor);

            var strToken = handler.WriteToken(token);

            return strToken;
        }
        private static ClaimsIdentity GenerateClaims(Usuario user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim("nome", user.Nome));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            

            return ci;
        }
    }
}
