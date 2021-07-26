using ApiRobustas.Compartilhados.Saude;

namespace ApiRobustas.Logs.Servicos
{
    /// <summary>
    /// Interface para os serviços de Log
    /// </summary>
    public interface ILogServico
    {
        InformacaoLog InformacaoLog { get; set; }
        void EscreverLog();
    }
}
