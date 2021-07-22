using ApiRobustas.Dominio.Contextos.Categorias.Entidades;
using ApiRobustas.Infraestrutura.Data.Mapeamentos.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiRobustas.Infraestrutura.Data.Mapeamentos.Efcore
{
    public class CategoriaMapeamento : MapeamentoBase<Categoria>
    {
        public override void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("CATEGORIA");

            builder.Property(c => c.Nome)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(c => c.Descricao)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Ignore(x => x.Notifications);

            base.ConfigurarEntidadeBase(builder);
        }
    }
}
