using ApiRobustas.Compartilhados.EntidadesBase;
using ApiRobustas.Compartilhados.ValidacoesDeDominio;
using ApiRobustas.Dominio.Contextos.Categorias.Recursos;
using Flunt.Notifications;
using Flunt.Validations;

namespace ApiRobustas.Dominio.Contextos.Categorias.Entidades
{
    public class Categoria : Entidade, IValidacaoEntidade
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        protected Categoria() { }

        public Categoria(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;

            ValidarEntidade();
        }

        public void AlterarNome(string nome) => Nome = nome;
        public void AlterarDescricao(string descricao) => Descricao = descricao;

        public void ValidarEntidade()
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
