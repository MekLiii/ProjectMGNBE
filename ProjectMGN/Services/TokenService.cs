using Microsoft.IdentityModel.Tokens;
using ProjectMGN.Interfaces;
using ProjectMGN.Models;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using ProjectMGN.DTOS.Request;

namespace ProjectMGN.Services
{
    public class TokenService : IToken
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public string GenerateToken(LoginRequest user)
        {
            var issuser = _configuration["Jwt:issuer"];
            var audience = _configuration["Jwt:audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            int expirationMinutes = Convert.ToInt32(_configuration["Jwt:ExpirationMinutes"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                NotBefore = DateTime.UtcNow,
                Issuer = issuser,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

    }
}
