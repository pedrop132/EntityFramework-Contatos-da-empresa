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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _Users;

        public AuthController(IConfiguration Users, AppDbContext context)
        {
            _Users = Users;
            _context = context;
        }

        public async Task<IActionResult> CreateUser(Users user)
        {


            // Simulate some async work
            await Task.CompletedTask;

            return Ok("User created successfully");
        }

        private string Create(Users user)
        {
            string secretKey = _Users["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.AccountId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
                ]),
            }
        }
    }
}
