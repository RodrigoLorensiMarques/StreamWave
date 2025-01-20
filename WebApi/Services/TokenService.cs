using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Services
{
    public class TokenService
    {
        private readonly string _secretKey;
        public TokenService(IConfiguration configuration)
        {
            _secretKey = configuration.GetSection("JwtSettings:SecretKey").Value;
        }

        public string GenerateJwtToken(string username, int userId, string Rule)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim ("id",userId.ToString()),
                    new Claim(ClaimTypes.Role, Rule)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "API-Login",
                Audience = "App",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}