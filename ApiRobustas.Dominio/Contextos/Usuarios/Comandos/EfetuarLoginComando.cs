using ApiRobustas.Compartilhados.ComandosBase;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace ApiRobustas.Dominio.Contextos.Usuarios.Comandos
{
    public class EfetuarLoginComando : Notifiable<Notification>, IComando, IRequest<IComandoResultado>
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public EfetuarLoginComando() { }

        public void ValidarComando()
        {
            AddNotifications(new Contract<Notification>()
              .Requires()
              .IsEmailOrEmpty(Email, nameof(Email), "o e-mail é inválido.")
              .IsNotNullOrEmpty(Senha, nameof(Senha), "A senha é obrigatória."));
        }
    }
}
