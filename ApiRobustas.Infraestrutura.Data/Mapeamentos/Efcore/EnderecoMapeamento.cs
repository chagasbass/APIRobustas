using ApiRobustas.Dominio.Contextos.Usuarios.Entidades;
using ApiRobustas.Infraestrutura.Data.Mapeamentos.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiRobustas.Infraestrutura.Data.Mapeamentos.Efcore
{
    public class EnderecoMapeamento : MapeamentoBase<Endereco>
    {
        public override void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("ENDERECO");

            builder.Property(e => e.CEP)
                   .HasMaxLength(8)
                   .IsRequired();

            builder.Property(e => e.Rua)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.Bairro)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Cidade)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Estado)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Ignore(x => x.Notifications);

            base.ConfigurarEntidadeBase(builder);
        }
    }
}
