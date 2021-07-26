using ApiRobustas.Compartilhados.Saude;
using System;

namespace ApiRobustas.Infraestrutura.Logs.Servicos
{
    /// <summary>
    /// Interface para os serviços de Log
    /// </summary>
    public interface ILogServico
    {
        InformacaoLog InformacaoLog { get; set; }
        void EscreverLog();
        void EscreverLogDeErros(Exception exception);
    }
}
