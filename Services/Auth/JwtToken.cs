using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using web_service.Models;

namespace web_service.Services.Auth
{
    public static class JwtToken
    {
        public static string GenerateToken(Usuario usuario)
        {
            
            
            var key = Encoding.ASCII.GetBytes(Secret.KEY);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Subject
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //new Claim(ClaimTypes.Username, usuario.Username.ToString()),
                }
                ),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.CreateToken(tokenDescriptor);

            return "";

        }
    }
}