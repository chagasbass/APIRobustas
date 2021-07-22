using ApiRobustas.Dominio.Contextos.Usuarios.Entidades;
using ApiRobustas.Infraestrutura.Data.Mapeamentos.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiRobustas.Infraestrutura.Data.Mapeamentos.Efcore
{
    public class UsuarioMapeamento : MapeamentoBase<Usuario>
    {
        public override void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO");

            #region objetos de valor
            builder.OwnsOne(u => u.Nome)
                   .Property(u => u.PrimeiroNome)
                   .HasColumnName("NOME")
                   .HasMaxLength(50)
                   .IsRequired();


            builder.OwnsOne(u => u.Nome)
                  .Property(u => u.Sobrenome)
                  .HasColumnName("SOBRENOME")
                  .HasMaxLength(50)
                  .IsRequired();

            #endregion

            builder.Property(u => u.Email)
                   .HasColumnName("EMAIL")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(u => u.Senha)
                   .HasColumnName("SENHA")
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(u => u.EnderecoId)
                   .HasColumnName("ID_ENDERECO")
                   .IsRequired();

            builder.HasOne(u => u.Endereco)
                   .WithMany()
                   .HasForeignKey(u => u.EnderecoId);

            builder.Ignore(x => x.Notifications);

            base.ConfigurarEntidadeBase(builder);
        }
    }
}
