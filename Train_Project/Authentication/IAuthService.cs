using Train_Project.DTOs.Auth;

namespace Train_Project.Authentication
{
    public interface IAuthService
    {
        string GenerateToken(int userId, string userName, string role);

        Task<bool> RegisterAsync(RegisterDto registerDto);

        Task<(string AccessToken, string RefreshToken)?> LoginAsync(LoginDto loginDto);

        Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);

        Task LogoutAsync(string refreshToken);
    }
}