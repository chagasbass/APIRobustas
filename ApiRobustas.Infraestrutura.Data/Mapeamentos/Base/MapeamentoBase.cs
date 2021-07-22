using ApiRobustas.Compartilhados.EntidadesBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiRobustas.Infraestrutura.Data.Mapeamentos.Base
{

    public abstract class MapeamentoBase<T> : IEntityTypeConfiguration<T> where T : Entidade
    {
        public abstract void Configure(EntityTypeBuilder<T> builder);

        public void ConfigurarEntidadeBase(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(c => c.DataCadastro)
                .HasColumnName("DATA_CADASTRO")
                .IsRequired();
        }
    }
}
