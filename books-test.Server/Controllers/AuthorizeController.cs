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

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new { message = "Username already taken" });
            }

            var citation1 = new Citation
            {
                Text = "To be, or not to be: that is the question.",
                UserUsername = user.Username
            };
            var citation2 = new Citation
            {
                Text = "So we beat on, boats against the current…",
                UserUsername = user.Username
            };
            var citation3 = new Citation
            {
                Text = "Don’t ever tell anybody anything.",
                UserUsername = user.Username
            };
            var citation4 = new Citation
            {
                Text = "You see, but you do not observe.",
                UserUsername = user.Username
            };
            var citation5 = new Citation
            {
                Text = "The secret of getting ahead is getting started.",
                UserUsername = user.Username
            };


            _context.Citations.Add(citation1);
            _context.Citations.Add(citation2);
            _context.Citations.Add(citation3);
            _context.Citations.Add(citation4);
            _context.Citations.Add(citation5);

            await _context.SaveChangesAsync();


            return Ok(new {message = "User registered"});
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