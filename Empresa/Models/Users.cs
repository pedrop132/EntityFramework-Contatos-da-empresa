using Empresa.Models;
using Microsoft.EntityFrameworkCore;
using Empresa.Controllers;

namespace Empresa.Models;

public class Users
{
    public int AccountId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public static class UserRepository
{
    public static async Task<Users?> GetByEmail(this DbSet<Users> users, string email)
    {
        return await users.FirstOrDefaultAsync(u => u.Email == email);
    }
    public static async Task<Users?> GetByPassword(this DbSet<Users> users, string password)
    {
        return await users.FirstOrDefaultAsync(u => u.Password == password);
    }
}