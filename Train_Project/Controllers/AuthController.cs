using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Train_Project.Authentication;
using Train_Project.Authentication.AuthEntity;
using Train_Project.Data;
using Train_Project.DTOs.Auth;

namespace Train_Project.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService, AppDbContext context) : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var tokens = await authService.LoginAsync(loginDto);

            if (tokens == null)
                return Unauthorized("Invalid username or password");

            return Ok(new
            {
                accessToken = tokens.Value.AccessToken,
                refreshToken = tokens.Value.RefreshToken
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await authService.RegisterAsync(registerDto);

            if (!result)
                return BadRequest("Username already exists");

            return Ok("Registration successful");
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenDto refreshTokenDto)
        {
            try
            {
                var tokens = await authService.RefreshTokenAsync(
                    refreshTokenDto.RefreshToken);

                return Ok(new
                {
                    accessToken = tokens.AccessToken,
                    refreshToken = tokens.RefreshToken
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid or expired refresh token");
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout(RefreshTokenDto refreshTokenDto)
        {
            await authService.LogoutAsync(refreshTokenDto.RefreshToken);

            return Ok("Logout successful");
        }
    }
}