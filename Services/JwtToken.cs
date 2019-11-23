using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using web_service.Models;

namespace web_service.Services.Auth
{
    public static class JwtToken
    {
        public static string GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(Secret.KEY);
            var securityKey = new SymmetricSecurityKey(key);
            
            var claims = new Claim[] { new Claim(ClaimTypes.Name, user.Username.ToString()) };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = 
                    new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(jwtToken);
        }
    }
}
