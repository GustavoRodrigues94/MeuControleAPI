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
            MapearUsuario(modelBuilder);
            MapearPlanoConta(modelBuilder);
            MapearLancamentoConta(modelBuilder);
        }

        private static void MapearUsuario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(x => x.Codigo);
            modelBuilder.Entity<Usuario>().Property(x => x.Nome).HasMaxLength(200).HasColumnType("varchar(200)");
            modelBuilder.Entity<Usuario>().Property(x => x.Email).HasMaxLength(300).HasColumnType("varchar(300)");
            modelBuilder.Entity<Usuario>().Property(x => x.Senha).HasMaxLength(50).HasColumnType("varchar(50)");
        }

        private static void MapearPlanoConta(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlanoConta>().HasKey(x => x.Codigo);
            modelBuilder.Entity<PlanoConta>().Property(x => x.Nome).HasMaxLength(200).HasColumnType("varchar(200)");
            modelBuilder.Entity<PlanoConta>().Property(x => x.Operacao).HasColumnType("varchar(1)");
            modelBuilder.Entity<PlanoConta>().HasIndex(x => x.CodigoUsuario);
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
