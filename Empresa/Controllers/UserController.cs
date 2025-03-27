using Empresa.Data;
using Empresa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Empresa.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;
        private readonly IConfiguration configuration;
        public UserController(AppDbContext context, IConfiguration _configuration)
        {
            _AppDbContext = context;
            configuration = _configuration;
        }

        [HttpGet("token")]
        public string CreateToken(Users user)
        {
            string secretKey = configuration["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.AccountId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
            ]),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

            var handler = new JsonWebTokenHandler();

            String token = handler.CreateToken(tokenDescriptor);

            return token;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Users user)
        {
            if (user == null)
                return BadRequest("Invalid user data.");

            _AppDbContext.Users.Add(user);
            await _AppDbContext.SaveChangesAsync();

            var token = CreateToken(user);

            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Users user)
        {
            if (user == null)
                return BadRequest("Invalid user data.");

            var existingUser = await _AppDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

            if (existingUser == null)
                return Unauthorized("Invalid credentials.");

            var token = CreateToken(existingUser);

            return Ok(new { Token = token });
        }

    }
}
