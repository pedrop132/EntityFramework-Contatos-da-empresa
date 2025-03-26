using Empresa.Data;
using Empresa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Empresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PrivateDbContext _context;
        private readonly IConfiguration _configs;

        public AuthController(IConfiguration configs, PrivateDbContext context)
        {
            _configs = configs;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Configs config)
        {
            var user = await _context.Users
                                      .FirstOrDefaultAsync(u => u.Username == config.Username && 
                                                                u.Password == config.Password);

            if (user == null) { 
                return Unauthorized("Invalid username or password"); 
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        private object GenerateJwtToken(Configs user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configs["Jwt:Issuer"],
                audience: _configs["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120), //120 minutes of token
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
