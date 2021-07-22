using ApiRobustas.Compartilhados.EntidadesBase;
using ApiRobustas.Compartilhados.ValidacoesDeDominio;

namespace ApiRobustas.Dominio.Contextos.Usuarios.Entidades
{
    public class Endereco : Entidade, IValidacaoEntidade
    {
        public string CEP { get; private set; }
        public string Rua { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        protected Endereco() { }

        public Endereco(string cep)
        {
            CEP = cep;

            ValidarEntidade();
        }

        public Endereco(string cep, string rua,
                        string bairro, string cidade, string estado)
        {
            ConfigurarCep(cep);
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        public void ValidarEntidade()
        {
            var cepEstaValido = ValidacoesCustomizadas.ValidarCep(CEP);

            if (cepEstaValido is false)
                AddNotification(nameof(CEP), "O cep informado é inválido");
        }

        private void ConfigurarCep(string cep) => CEP = cep.Replace("-", string.Empty);

        public override string ToString() => $"{Rua} {Bairro} {Cidade} {CEP} {Estado}";
    }
}
