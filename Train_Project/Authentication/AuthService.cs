using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Train_Project.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly JwtOption _jwtOption;

        public AuthService(IOptions<JwtOption> jwtOption)
        {
            _jwtOption = jwtOption.Value;
        }

        public string GenerateToken(int userId, string userName, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOption.Issuer,
                Audience = _jwtOption.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.SignKey)),
                    SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()), 
                new Claim(ClaimTypes.Name, userName), 
                new Claim(ClaimTypes.Role, role)
            }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOption.Lifetime)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); 
        }
    }
}
