using Empresa.Models;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ContatosEmpresa> Contatos { get; set; }

    }

    // For login
    public class PrivateDbContext : DbContext
    {
        public PrivateDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Configs> Users { get; set; } 
    }
}
