using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Compartilhados.ValidacoesDeDominio;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace ApiRobustas.Dominio.Contextos.Usuarios.Comandos
{
    public class CadastrarUsuarioComando : Notifiable<Notification>, IComando, IRequest<IComandoResultado>
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cep { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public CadastrarUsuarioComando() { }

        public void ValidarComando()
        {
            AddNotifications(new Contract<Notification>()
               .Requires()
               .IsNotNullOrEmpty(Nome, nameof(Nome), "O nome é obrigatório")
               .IsNotMaxValue(Nome.Length, nameof(Nome), "O nome deve conter no máximo 50 caracteres")
               .IsNotNullOrEmpty(Sobrenome, nameof(Sobrenome), "O sobrenome é obrigatório")
               .IsNotMaxValue(Sobrenome.Length, nameof(Sobrenome), "O sobrenome deve conter no máximo 50 caracteres")
               .IsEmailOrEmpty(Email, nameof(Email), "o e-mail é inválido.")
               .IsNotNullOrEmpty(Senha, nameof(Senha), "A senha é obrigatória."));

            if (!ValidacoesCustomizadas.ValidarCep(Cep))
                AddNotification("Cep", "O cep é inválido.");
        }
    }
}
