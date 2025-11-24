using books_test.Server.Data;
using books_test.Server.Model;
using books_test.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace books_test.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;

        public AuthorizeController(ApplicationDBContext context, IConfiguration configuration, TokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict("Username already exists");
            }

            return Ok("User registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == login.Username && u.Password == login.Password);
            if (user == null) return Unauthorized();

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user.Username);

            return Ok(new { accessToken, refreshToken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenModel model)
        {
            var refreshToken = _tokenService.GetRefreshToken(model.RefreshToken);
            if (refreshToken == null)
                return Unauthorized();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == refreshToken.UserUsername);
            if (user == null) return Unauthorized();

            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken(user.Username);

            // Revoke the old refresh token
            _tokenService.RevokeRefreshToken(refreshToken.RefreshUserToken);

            return Ok(new { accessToken = newAccessToken, refreshToken = newRefreshToken });
        }   

    }
}