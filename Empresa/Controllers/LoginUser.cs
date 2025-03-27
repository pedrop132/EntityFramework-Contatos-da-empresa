using Empresa.Data;
using Empresa.Models;

namespace Empresa.Controllers;

internal sealed class LoginUser(AppDbContext context, tokenProvider tokenProvider)
{
    public sealed record Request(string Email, String Password);

    public async Task<string> Handle(Request request)
    {
        Users? user = await context.Users.GetByEmail(request.Email);

        if (user is null)
        {
            throw new Exception("Email incorreto");
        }

        string token = tokenProvider.Create(user);

        return token; // Authentication successful
    }
}
