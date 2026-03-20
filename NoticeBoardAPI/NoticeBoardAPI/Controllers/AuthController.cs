using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using NoticeBoardAPI.Interfaces;
using NoticeBoardAPI.Models;
using NoticeBoardAPI.Repositories;
using NoticeBoardAPI.Services;
using NoticeBoardAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace NoticeBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserRepository userRepository, IAuthService authorizationService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _authService = authorizationService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] GoogleLoginRequest googleLoginRequest)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _configuration["GoogleAuth:ClientId"] }
                };
                
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginRequest.IdToken, settings);

                var user = await _userRepository.GetByGoogleIdAsync(payload.Subject);

                if (user == null)
                {
                    user = await _userRepository.CreateAsync(new UserModel
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

                var token = _authService.GenerateToken(user.Id, user.Email, user.DisplayName);

                return Ok(new
                {
                    token,
                    user = new
                    {
                        user.Id,
                        user.Email,
                        user.DisplayName,
                        user.AvatarUrl
                    }
                });
            }
            catch (InvalidJwtException)
            {
                return Unauthorized("Невалідний Google токен");
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"]
                .FirstOrDefault()?.Split(" ").Last();
            
            if(token != null)
            {
                _authService.BlacklistToken(token);
            }

            return Ok(new { message = "Успішний вихід" });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if(userIdString == null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdString);
            var user = await _userRepository.GetByIdAsync(userId);
            if(user == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                user.Id,
                user.Email,
                user.DisplayName,
                user.AvatarUrl
            });
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
                    user = await _userRepository.CreateAsync(new Models.UserModel
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

                var token = _authService.GenerateToken(user.Id, user.Email, user.DisplayName);
                return Ok(new { token });
            }
            catch (InvalidJwtException)
            {
                return Unauthorized("Невалідний Google токен");
            }
        }
    }
}
