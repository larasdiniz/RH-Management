using Microsoft.EntityFrameworkCore;
using RH_Management.API.Models;

namespace RH_Management.API.Data
{
    public class RHContext : DbContext
    {
        public RHContext(DbContextOptions<RHContext> options) : base(options) { }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Ferias> Ferias { get; set; }
        public DbSet<HistoricoAlteracao> HistoricoAlteracoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeia as classes para os nomes de tabela no banco
            modelBuilder.Entity<Funcionario>().ToTable("Funcionario");
            modelBuilder.Entity<Ferias>().ToTable("Ferias");
            modelBuilder.Entity<HistoricoAlteracao>().ToTable("HistoricoAlteracao");

            // Configurações de relacionamento
            modelBuilder.Entity<Ferias>()
                .HasOne(f => f.Funcionario)
                .WithMany(f => f.Ferias)
                .HasForeignKey(f => f.FuncionarioId);

            modelBuilder.Entity<HistoricoAlteracao>()
                .HasOne(h => h.Funcionario)
                .WithMany(f => f.HistoricoAlteracoes)
                .HasForeignKey(h => h.FuncionarioId);
        }
    }
}