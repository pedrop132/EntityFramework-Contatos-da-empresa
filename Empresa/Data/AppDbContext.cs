using Empresa.Models;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ContatosEmpresa> Contatos { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
