using MeuControle.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuControle.Infra.Mapeamentos
{
    public class LancamentoContaMapeamento : IEntityTypeConfiguration<LancamentoConta>
    {
        public void Configure(EntityTypeBuilder<LancamentoConta> builder)
        {
            builder.HasKey(x => x.Codigo);
            builder.Property(x => x.DataLancamento).HasColumnType("date");
            builder.Property(x => x.Operacao).HasColumnType("varchar(1)");
            builder.Property(x => x.Valor).HasColumnType("decimal(8,2)");
            builder.HasOne(x => x.Usuario).WithMany(b => b.LancamentosContas).HasForeignKey(x => x.CodigoUsuario);
            builder.HasOne(x => x.PlanoConta).WithMany(b => b.LancamentosContas).HasForeignKey(x => x.CodigoPlanoConta);
        }
    }
}
