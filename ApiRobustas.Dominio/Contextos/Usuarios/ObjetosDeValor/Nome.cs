using ApiRobustas.Compartilhados.EntidadesBase;
using ApiRobustas.Compartilhados.ValidacoesDeDominio;
using Flunt.Notifications;
using Flunt.Validations;

namespace ApiRobustas.Dominio.Contextos.Usuarios.ObjetosDeValor
{
    public class Nome : ObjetoDeValor, IValidacaoEntidade
    {
        public string PrimeiroNome { get; private set; }
        public string Sobrenome { get; private set; }

        public Nome(string primeiroNome, string sobrenome)
        {
            PrimeiroNome = primeiroNome;
            Sobrenome = sobrenome;

            ValidarEntidade();
        }

        public Nome AlterarPrimeiroNome(string primeiroNome) => new Nome(primeiroNome, Sobrenome);
        public Nome AlterarSobreNome(string sobrenome) => new Nome(PrimeiroNome, sobrenome);

        public void ValidarEntidade()
        {
            AddNotifications(new Contract<Notification>()
               .Requires()
               .IsNotNullOrEmpty(PrimeiroNome, nameof(PrimeiroNome), "O nome é obrigatório")
               .IsNotMaxValue(PrimeiroNome.Length, nameof(PrimeiroNome), "O nome deve conter no máximo 50 caracteres")
               .IsNotNullOrEmpty(Sobrenome, nameof(Sobrenome), "O sobrenome é obrigatório")
               .IsNotMaxValue(Sobrenome.Length, nameof(Sobrenome), "O sobrenome deve conter no máximo 50 caracteres"));
        }
    }
}
