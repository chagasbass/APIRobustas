using ApiRobustas.Compartilhados.Saude;
using Serilog;

namespace ApiRobustas.Logs.Servicos
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
            _logger.Information($"[TimeStamp]:{InformacaoLog.Timestamp}");
            _logger.Information($"[Usuario]: {InformacaoLog.Usuario ?? "Usuário não está logado."}");
            _logger.Information($"[ExternalUri]: { InformacaoLog.ExternalUri ?? "Sem Uri"}");
            _logger.Information($"[RequestQueryParams]: { InformacaoLog.RequestQueryParams ?? "Sem parametros"}");
            _logger.Information($"[ExternalRequestBody]: { InformacaoLog.ExternalRequestBody ?? "Sem body na requisição externa"}");
            _logger.Information($"[ExternalQueryParams]: { InformacaoLog.ExternalQueryParams ?? "Sem parâmetros na requisição externa"}");
            _logger.Information($"[ExternalResponseBody]: { InformacaoLog.ExternalResponseBody ?? "Sem body na response externa"}");
            _logger.Information($"[RequestBody]: { InformacaoLog.RequestBody ?? "Requisição sem body"}");
            _logger.Information($"[ResponseBody]: { InformacaoLog.ResponseBody}");
            _logger.Information($"[Response information] {InformacaoLog.Method}");
            _logger.Information($"[StatusCode]: {InformacaoLog.ResponseStatusCode}");
            _logger.Information($"[TraceId]:{InformacaoLog.TraceId}");
            _logger.Information($"[RequestUriParams]:{InformacaoLog.RequestQueryParams}");
        }
    }
}
