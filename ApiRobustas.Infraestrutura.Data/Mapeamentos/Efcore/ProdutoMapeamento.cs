using ApiRobustas.Dominio.Contextos.Produtos.Entidades;
using ApiRobustas.Infraestrutura.Data.Mapeamentos.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiRobustas.Infraestrutura.Data.Mapeamentos.Efcore
{
    public class ProdutoMapeamento : MapeamentoBase<Produto>
    {
        public override void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("PRODUTO");

            builder.Property(p => p.CategoriaId)
                   .HasColumnName("ID_CATEGORIA")
                   .IsRequired();

            builder.Property(p => p.UsuarioId)
                   .HasColumnName("ID_USUARIO")
                   .IsRequired();

            builder.Property(p => p.Nome)
                   .HasColumnName("NOME")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(p => p.Descricao)
                   .HasColumnName("DESCRICAO")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(p => p.Preco)
                   .HasColumnName("PRECO")
                   .HasColumnType("DECIMAL(19,4")
                   .IsRequired();

            builder.Property(p => p.Quantidade)
                   .HasColumnName("QUANTIDADE")
                   .IsRequired();

            builder.HasOne(p => p.Categoria)
                   .WithMany()
                   .HasForeignKey(p => p.CategoriaId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Usuario)
                   .WithMany()
                   .HasForeignKey(p => p.UsuarioId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Ignore(x => x.Notifications);

            base.ConfigurarEntidadeBase(builder);
        }
    }
}
