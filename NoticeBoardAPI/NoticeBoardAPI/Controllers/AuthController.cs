using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using NoticeBoardAPI.Models;
using NoticeBoardAPI.Repositories;
using NoticeBoardAPI.Services;
using NoticeBoardAPI.DTOs;

namespace NoticeBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthController(UserRepository userRepository, TokenService tokenService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _configuration["GoogleAuth:ClientId"] }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
                var user = await _userRepository.GetByGoogleIdAsync(payload.Subject);

                if (user == null)
                {
                    user = await _userRepository.CreateAsync(new Models.User
                    {
                        GoogleId = payload.Subject,
                        Email = payload.Email,
                        DisplayName = payload.Name,
                        AvatarUrl = payload.Picture
                    });
                }
                else
                {
                    await _userRepository.UpdateLastLoginAsync(user.Id);
                }

                var token = _tokenService.GenerateToken(user.Id, user.Email, user.DisplayName);
                return Ok(new { token });
            }
            catch (InvalidJwtException)
            {
                return Unauthorized("Невалідний Google токен");
            }
        }
    }
}
