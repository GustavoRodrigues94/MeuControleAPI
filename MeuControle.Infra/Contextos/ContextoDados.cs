using MeuControle.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MeuControle.Infra.Contextos
{
    public class ContextoDados : DbContext
    {
        public ContextoDados(DbContextOptions<ContextoDados> opcoes) : base (opcoes)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PlanoConta> PlanosContas { get; set; }
        public DbSet<LancamentoConta> LancamentosContas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoDados).Assembly);
        }

        private static void MapearLancamentoConta(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LancamentoConta>().HasKey(x => x.Codigo);
            modelBuilder.Entity<LancamentoConta>().Property(x => x.DataLancamento).HasColumnType("date");
            modelBuilder.Entity<LancamentoConta>().Property(x => x.Operacao).HasColumnType("varchar(1)");
            modelBuilder.Entity<LancamentoConta>().Property(x => x.Valor).HasColumnType("decimal(8,2)");
            modelBuilder.Entity<LancamentoConta>().HasOne(x => x.Usuario).WithMany(b => b.LancamentosContas).HasForeignKey(x => x.CodigoUsuario);
            modelBuilder.Entity<LancamentoConta>().HasOne(x => x.PlanoConta).WithMany(b => b.LancamentosContas).HasForeignKey(x => x.CodigoPlanoConta);
        }
    }
}
