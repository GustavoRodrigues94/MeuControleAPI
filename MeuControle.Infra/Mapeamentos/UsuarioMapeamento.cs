using MeuControle.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuControle.Infra.Mapeamentos
{
    public class UsuarioMapeamento : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Codigo);
            builder.Property(x => x.Nome).HasMaxLength(200).HasColumnType("varchar(200)");
            builder.Property(x => x.Email).HasMaxLength(300).HasColumnType("varchar(300)");
            builder.Property(x => x.Senha).HasMaxLength(50).HasColumnType("varchar(50)");
            builder.Property(x => x.RendaMensal).HasColumnType("decimal(8,2)");
        }
    }
}
