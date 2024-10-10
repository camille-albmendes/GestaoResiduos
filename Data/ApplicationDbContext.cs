using GestaoDeResiduos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeResiduos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Definir DbSet para Resíduos
        public DbSet<Residencia> Residencias { get; set; }
        public DbSet<Resíduo> Resíduos { get; set; }
    }
}
