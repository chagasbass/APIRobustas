using ApiRobustas.Compartilhados.ComandosBase;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System;

namespace ApiRobustas.Dominio.Contextos.Produtos.Comandos
{
    public class AtualizarProdutoComando : Notifiable<Notification>, IComando, IRequest<IComandoResultado>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }

        public void ValidarComando()
        {
            AddNotifications(new Contract<Notification>()
              .Requires()
              .IsNotNullOrEmpty(Nome, nameof(Nome), "O nome é obrigatório.")
              .IsNotNullOrEmpty(Descricao, nameof(Descricao), "A descrição é obrigatória.")
              .IsGreaterOrEqualsThan(Preco, 0, "O preço é inválida.")
              .IsGreaterOrEqualsThan(Quantidade, 0, "A quantidade é inválida."));
        }
    }
}
