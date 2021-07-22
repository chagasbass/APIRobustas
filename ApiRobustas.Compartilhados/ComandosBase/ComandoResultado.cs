namespace ApiRobustas.Compartilhados.ComandosBase
{
    /// <summary>
    /// Classe que representa o retorno customizada da aplicação
    /// </summary>
    public class ComandoResultado : IComandoResultado
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Data { get; set; }

        public ComandoResultado() { }

        public ComandoResultado(bool sucesso, string mensagem = null, object data = null)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Data = data;
        }
    }
}
