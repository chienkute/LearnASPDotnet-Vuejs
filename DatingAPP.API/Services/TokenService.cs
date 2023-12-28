using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DatingApp.API.Databases.Entities;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly string jwtSecret;
        public TokenService(IConfiguration configuration)
        {
            jwtSecret = configuration.GetValue<string>("JwtSecretKey")!;
        }

        public string GenerateJWT(User user)
        {
            var claims = new List<Claim>()
            {
                new (JwtRegisteredClaimNames.NameId, user.Username),
                new (JwtRegisteredClaimNames.Email, user.Email ?? "")
            };

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new(symmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}