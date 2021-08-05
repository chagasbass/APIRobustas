using ApiRobustas.Compartilhados.Saude;
using Serilog;
using System;

namespace ApiRobustas.Infraestrutura.Logs.Servicos
{
    public class LogServico : ILogServico
    {
        public InformacaoLog InformacaoLog { get; set; }

        private readonly ILogger _logger = Log.ForContext<LogServico>();

        public LogServico()
        {
            InformacaoLog = new InformacaoLog();
        }

        public void EscreverLog()
        {
            _logger.ForContext("[TimeStamp]:", InformacaoLog.Timestamp)
                   .ForContext("[IpAddress]:", InformacaoLog.IpAddress.ToString() ?? "Não informado")
                   .ForContext("[Usuario]:", InformacaoLog.Usuario ?? "Usuário não está logado.")
                   .ForContext("[ExternalUri]:", InformacaoLog.ExternalUri ?? "Sem Uri")
                   .ForContext("[Endpoint]: ", InformacaoLog.Endpoint)
                   .ForContext("[Controller]:", InformacaoLog.Controller)
                   .ForContext("[RequestQueryParams]:", InformacaoLog.RequestQueryParams ?? "Sem parametros")
                   .ForContext("[ExternalRequestBody]:", InformacaoLog.ExternalRequestBody ?? "Sem body na requisição externa")
                   .ForContext("[ExternalQueryParams]: ", InformacaoLog.ExternalQueryParams ?? "Sem parâmetros na requisição externa")
                   .ForContext("[ExternalResponseBody]: ", InformacaoLog.ExternalResponseBody ?? "Sem body na response externa")
                   .ForContext("[RequestBody]:", InformacaoLog.RequestBody ?? "Requisição sem body")
                   .ForContext("[ResponseBody]:", InformacaoLog.ResponseBody)
                   .ForContext("[Response information]", InformacaoLog.Method)
                   .ForContext("[StatusCode]: ", InformacaoLog.ResponseStatusCode)
                   .ForContext("[TraceId]:", InformacaoLog.TraceId)
                   .ForContext("[RequestUriParams]:", InformacaoLog.RequestQueryParams)
                   .Information("Log da Aplicação");
        }

        public void EscreverLogDeErros(Exception exception)
        {
            _logger.ForContext("[Exception]:", exception.GetType().Name)
                   .ForContext("[Message]:", exception.Message)
                   .ForContext("[ExceptionStackTrace]:", exception.StackTrace)
                   .ForContext("[InnerException]:", exception?.InnerException?.Message)
                   .ForContext("[Stack Chamadas]:", Environment.StackTrace)
                   .Error("Log da Aplicação");
        }
    }
}
