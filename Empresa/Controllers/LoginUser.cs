using Empresa.Data;
using Empresa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Controllers;

internal sealed class LoginUser(AppDbContext context, tokenProvider tokenProvider)
{
    public sealed record Request(string Email, String Password);

    public async Task<string> Handle(Request request, string password)
    {
        Users? user = await context.Users.GetByEmail(request.Email);

        if (user is null)
        {
            throw new Exception("Email incorreto");
        }

        var passwordHasher = new PasswordHasher<Users>();
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, user.Password, password);

        if (passwordVerificationResult != PasswordVerificationResult.Success)
        {
            throw new Exception("Password incorreta");
        }

        string token = tokenProvider.Create(user);

        return token; // Authentication successful
    }
}
