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
            _logger.Information($"[TimeStamp]:{ InformacaoLog.Timestamp}");
            _logger.Information($"[IpAddress]:{InformacaoLog.IpAddress.ToString()}");
            _logger.Information($"[Usuario]:{ InformacaoLog.Usuario}");
            _logger.Information($"[ExternalUri]:{InformacaoLog.InformacaoExternaLog.ExternalRequestUri}");
            _logger.Information($"[Endpoint]: { InformacaoLog.Endpoint}");
            _logger.Information($"[Controller]: {InformacaoLog.Controller}");
            _logger.Information($"Log da aplicação");

            if (InformacaoLog.InformacaoExternaLog.VerficarSeInformacoesExternasExistem())
            {
                _logger.Information($"[RequestQueryParams]: {InformacaoLog.RequestQueryParams}");
                _logger.Information($"[ExternalRequestBody]: {InformacaoLog.InformacaoExternaLog.ExternalRequestBody}");
                _logger.Information($"[ExternalQueryParams]: {InformacaoLog.InformacaoExternaLog.ExternalRequestParams}");
                _logger.Information($"[ExternalResponseBody]: {InformacaoLog.InformacaoExternaLog.ExternalResponseBody}");
            }

            if (!string.IsNullOrEmpty(InformacaoLog.RequestBody))
            {
                _logger.Information($"[RequestBody]:{InformacaoLog.RequestBody}");
            }

            if (!string.IsNullOrEmpty(InformacaoLog.RequestBody))
            {
                _logger.Information($"[ResponseBody]: {InformacaoLog.ResponseBody}");
            }

            _logger.Information($"[Response information]:{InformacaoLog.Method}");

            if (InformacaoLog.ResponseStatusCode != 0)
            {
                _logger.Information($"[StatusCode]: {InformacaoLog.ResponseStatusCode}");
            }

            _logger.Information($"[TraceId]: {InformacaoLog.TraceId}");
            _logger.Information($"[RequestUriParams]:{ InformacaoLog.RequestQueryParams}");
            _logger.Information("Log da Aplicação");
        }

        public void EscreverLogDeErros(Exception exception)
        {
            _logger.Error($"[Exception]: {exception.GetType().Name}");
            _logger.Error($"[Message]: { exception.Message}");
            _logger.Error($"[ExceptionStackTrace]: { exception.StackTrace}");
            _logger.Error($"[InnerException]: {exception?.InnerException?.Message}");
            _logger.Error($"[Stack Chamadas]: { Environment.StackTrace}");
            _logger.Error("Log da Aplicação");
        }
    }
}
