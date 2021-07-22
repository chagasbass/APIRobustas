using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Dominio.Contextos.Categorias.Recursos;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace ApiRobustas.Dominio.Contextos.Categorias.Comandos
{
    public class CadastrarCategoriaComando : Notifiable<Notification>, IComando, IRequest<IComandoResultado>
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public CadastrarCategoriaComando() { }

        public void ValidarComando()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Nome, nameof(Nome), MensagensDeCategoria.NomeNaoPreenchido)
                .IsNotMaxValue(Nome.Length, nameof(Nome), MensagensDeCategoria.NomeInvalido)
                .IsNotNullOrEmpty(Descricao, nameof(Descricao), MensagensDeCategoria.DescricaoNaoPreenchida)
                .IsNotMaxValue(Nome.Length, nameof(Nome), MensagensDeCategoria.DescricaoInvalida));
        }
    }
}
