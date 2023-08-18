using Microsoft.IdentityModel.Tokens;
using ProjectMGN.Models;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using ProjectMGN.DTOS.Request;
using ProjectMGN.Interfaces.Services;

namespace ProjectMGN.Services
{
    public class TokenService : IToken
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public int UserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null)
            {
                throw new Exception("Token is null");
            }
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid");
            if (userIdClaim?.Value == null)
            {
                throw new Exception("Something went wrong with token");
            }
            var userId = int.Parse(userIdClaim.Value);
           

            return userId;
        }
        public string GenerateToken(User user)
        {
            var issuser = _configuration["Jwt:issuer"];
            var audience = _configuration["Jwt:audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            int expirationMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Guid", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.Now.AddMinutes(expirationMinutes),
                NotBefore = DateTime.Now,
                Issuer = issuser,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
        public int? ValidateToken(string token)
        {
            if (token == null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                int userId = int.Parse(jwtToken.Claims.First(x => x.Type == "nameid").Value);

                return userId;
            }
            catch
            {
                return null;
            }
        }

    }
}
