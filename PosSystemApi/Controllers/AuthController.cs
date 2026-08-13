using Microsoft.AspNetCore.Mvc;
using PosSystemApi.Authentication;
using PosSystemApi.DTOs;

namespace PosSystemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var user = await _authService.RegisterAsync(request);

                return Ok(new
                {
                    user.UserId,
                    user.Name,
                    user.Email,
                    user.Role
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            string? token = await _authService.LoginAsync(request);

            if (token == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(new
            {
                Token = token
            });
        }
    }
}