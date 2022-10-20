using ApiWithAuthentication.Configurations;
using ApiWithAuthentication.Models;
using ApiWithAuthentication.Services.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiWithAuthentication.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfiguration _jwtConfiguration;

        public TokenService(JwtConfiguration jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // You need to convert to bytes
            var key = Encoding.ASCII.GetBytes(_jwtConfiguration.JwtKey);

            // Set Lifetime and signature
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] // Subject is the payload
                {
                    new (ClaimTypes.Name, user.Id.ToString()), // User.Identity.Name
                    new (ClaimTypes.Role, user.Role.Name), // User.IsInRole
                    new ("SomeKey", "SomeValue") // Custom data
                }),
                Expires = DateTime.UtcNow.AddSeconds(_jwtConfiguration.LifeTimeInSeconds),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
