using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;
using System;

namespace ApiRobustas.Api.Configuracoes
{
    /// <summary>
    /// Configuração dos logs da aplicação
    /// </summary>
    public static class LogConfig
    {

        private const string MESSAGE_TEMPLATE =
          "CHAMADA  {RequestMethod} para '{Path}' foi respondida com {StatusCode} em {Elapsed:0} ms";

        /// <summary>
        /// COnfiguração de log para requests
        /// </summary>
        /// <param name="app"></param>
        public static void UseRequestLogConfiguration(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.MessageTemplate = MESSAGE_TEMPLATE;
                options.GetLevel = RequestLogLevel;
                options.EnrichDiagnosticContext = EnrichRequest;
            });
        }

        /// <summary>
        /// COnfiguração dos dados do log
        /// </summary>
        /// <param name="diagnosticCtx"></param>
        /// <param name="httpCtx"></param>
        private static void EnrichRequest(IDiagnosticContext diagnosticCtx, HttpContext httpCtx)
        {
            diagnosticCtx.Set("[Dados da Requisição]", string.Empty);
            diagnosticCtx.Set("[Ip da máquina]", httpCtx.Connection.RemoteIpAddress?.ToString() ?? "- Não Conhecido -");
            diagnosticCtx.Set("[Usuário]", httpCtx.User.Identity?.Name ?? "- Não autenticado -");
            diagnosticCtx.Set("[Método]", httpCtx.Request.Method);

            if (httpCtx.Request.Path.HasValue)
                diagnosticCtx.Set("[Path]", httpCtx.Request.Path.Value);

            if (httpCtx.Request.QueryString.HasValue)
                diagnosticCtx.Set("[QueryString]", httpCtx.Request.QueryString);

            var endpoint = httpCtx.GetEndpoint();

            if (endpoint != null)
                diagnosticCtx.Set("[Nome do Endpoint]", endpoint.DisplayName);

            diagnosticCtx.Set("[Response StatusCode]", httpCtx.Response.StatusCode);
        }

        private static LogEventLevel RequestLogLevel(HttpContext httpCtx, double elapsedMs, Exception ex)
        {
            if (ex != null || httpCtx.Response.StatusCode >= 500)
                return LogEventLevel.Error;

            if (httpCtx.Request.Path == "/service-worker.js" || httpCtx.Request.Method == "OPTIONS"
                || httpCtx.Request.Path == "/healthchecks-data-ui" ||
                httpCtx.Request.Path == "/monitor" ||
                httpCtx.Request.Path == "/app-status")
                return LogEventLevel.Debug;

            return LogEventLevel.Information;
        }
    }
}
