using AttCadastro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AttCadastro.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options)
            : base(options)
        {
        }

        // 🔹 Tabela do banco de dados
        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Evita warnings de PendingModelChanges (mas apenas no desenvolvimento)
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Configuração da tabela Pessoa
            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.ToTable("Pessoas");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Nome)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.Senha)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(p => p.Cargo)
                      .HasMaxLength(50);
            });
        }
    }
}
