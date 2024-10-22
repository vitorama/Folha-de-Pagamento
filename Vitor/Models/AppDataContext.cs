using Microsoft.EntityFrameworkCore;

namespace Vitor.Models
{
    public class AppDataContext : DbContext
    {
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Folha> Folhas { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Alisson_Vitor.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folha>()
                .HasOne(f => f.Funcionario)
                .WithMany()
                .HasForeignKey(f => f.FuncionarioId);
        }
    }
}
