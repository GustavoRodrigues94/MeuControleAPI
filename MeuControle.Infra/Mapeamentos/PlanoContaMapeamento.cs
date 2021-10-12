using MeuControle.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuControle.Infra.Mapeamentos
{
    public class PlanoContaMapeamento : IEntityTypeConfiguration<PlanoConta>
    {
        public void Configure(EntityTypeBuilder<PlanoConta> builder)
        {
            builder.HasKey(x => x.Codigo);
            builder.Property(x => x.Nome).HasMaxLength(200).HasColumnType("varchar(200)");
            builder.Property(x => x.Operacao).HasColumnType("varchar(1)");
            builder.HasIndex(x => x.CodigoUsuario);
        }
    }
}
