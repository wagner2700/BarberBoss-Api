using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarberBoss.Infraestructure.Security
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly uint _expiresTimeInMinutes;
        private readonly string _signingKey;

        public TokenGenerator(uint expiresTimeInMinutes, string signingKey)
        {
            _expiresTimeInMinutes = expiresTimeInMinutes;
            _signingKey = signingKey;
        }
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.Name),
                new Claim(ClaimTypes.Sid , user.UserIdentifier.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(_expiresTimeInMinutes),
                SigningCredentials = new SigningCredentials(securityKey(), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims)

            };

            var tokenHandler = new JwtSecurityTokenHandler();  
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        private SymmetricSecurityKey securityKey()
        {
            var key = Encoding.UTF8.GetBytes(_signingKey);
            return new SymmetricSecurityKey(key);
        }
    }
}
