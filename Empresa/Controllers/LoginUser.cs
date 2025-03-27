using Empresa.Data;
using Empresa.Models;

namespace Empresa.Controllers;

internal sealed class LoginUser(AppDbContext context)
{
    public sealed record Request(string Email, String Password);

    public async Task<Users> Handle(Request request)
    {
        Users? user = await context.Users.GetByEmail(request.Email);

        if (user is null)
        {
            throw new Exception("Email incorreto");
        }

        string token = tokenProvider.Create(user);

        return user; // Authentication successful
    }
}
