using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Train_Project.Data;
using Train_Project.Authentication.AuthEntity;
using Train_Project.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using Train_Project.Entities;

namespace Train_Project.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly JwtOption _jwtOption;

        private readonly AppDbContext _context;

        private readonly IPasswordHasher<Users> _passwordHasher;

        public AuthService(
     IOptions<JwtOption> jwtOption,
     AppDbContext context,
     IPasswordHasher<Users> passwordHasher)
        {
            _jwtOption = jwtOption.Value;
            _context = context;
            _passwordHasher = passwordHasher;
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
        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == registerDto.Username);

            if (existingUser != null)
                return false;

            var customer = new Customer
            {
                Name = registerDto.Name,
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = "N/A",
                Location = registerDto.Location
            };

            var user = new Users
            {
                Username = registerDto.Username,
                Password = _passwordHasher.HashPassword(null!, registerDto.Password),
                Roles = Roles.Customer
            };

            _context.Customers.Add(customer);
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return true;
        }



        public async Task<(string AccessToken, string RefreshToken)?> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == loginDto.Username);

            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                loginDto.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            var accessToken = GenerateToken(
                user.Id,
                user.Username,
                user.Roles);

            var refreshToken = Guid.NewGuid().ToString();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return (accessToken, refreshToken);
        }


        public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.RefreshToken == refreshToken &&
                    x.RefreshTokenExpiryTime > DateTime.UtcNow);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            var accessToken = GenerateToken(
                user.Id,
                user.Username,
                user.Roles);

            var newRefreshToken = Guid.NewGuid().ToString();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return (accessToken, newRefreshToken);
        }


        public async Task LogoutAsync(string refreshToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

            if (user == null)
                return;

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await _context.SaveChangesAsync();
        }

    }
}
