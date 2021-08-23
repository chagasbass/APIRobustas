using ApiRobustas.Infraestrutura.Logs.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRobustas.Api.Middlewares
{
    /// <summary>
    ///Middleware para tratamento de logs na aplicação
    /// </summary>
    public class SerilogRequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogServico _logServico;
        private readonly IDiagnosticContext _diagnosticContext;

        public SerilogRequestLoggerMiddleware(RequestDelegate next,
                                              ILogServico logServico,
                                              IDiagnosticContext diagnosticContext)
        {
            _logServico = logServico;
            _diagnosticContext = diagnosticContext;

            if (next == null) throw new ArgumentNullException(nameof(next));
            _next = next;
        }

        /// <summary>
        /// Efetua a leitura do HttpContext para recuperar as informações de request e response para os logs
        /// da aplicação.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            //efetuando a leitura do request pois ele vem como um stream.
            string requestBody = await RecuperarRequestBodyAsync(context);

            var originalResponseBodyReference = context.Response.Body;

            using var responseBodyMemoryStream = new MemoryStream();

            var responseBody = await RecuperarResponseBodyAsync(context, responseBodyMemoryStream);

            CriarInformacoesDeLog(requestBody, responseBody, context);

            await responseBodyMemoryStream.CopyToAsync(originalResponseBodyReference);
        }

        /// <summary>
        /// Recupera o body recebido na requisição
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private async Task<string> RecuperarRequestBodyAsync(HttpContext httpContext)
        {
            HttpRequestRewindExtensions.EnableBuffering(httpContext.Request);
            Stream body = httpContext.Request.Body;
            byte[] buffer = new byte[Convert.ToInt32(httpContext.Request.ContentLength)];

            await httpContext.Request.Body.ReadAsync(buffer, 0, buffer.Length);

            var requestBody = Encoding.UTF8.GetString(buffer);

            body.Seek(0, SeekOrigin.Begin);

            httpContext.Request.Body = body;

            return requestBody;
        }

        /// <summary>
        /// recupera o Response de retorno da requisição
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="responseBodyMemoryStream"></param>
        /// <returns></returns>
        private async Task<string> RecuperarResponseBodyAsync(HttpContext httpContext, MemoryStream responseBodyMemoryStream)
        {
            httpContext.Response.Body = responseBodyMemoryStream;

            await _next(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

            var responseBody = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

            return responseBody;
        }

        /// <summary>
        /// Efetua a criação do log estruturado
        /// </summary>
        /// <param name="requestBody"></param>
        /// <param name="responseBody"></param>
        /// <param name="httpContext"></param>
        private void CriarInformacoesDeLog(string requestBody, string responseBody, HttpContext httpContext)
        {
            RecuperarQueryParamsDoRequest(httpContext);

            var endpoint = httpContext.GetEndpoint();
            if (endpoint != null)
                _logServico.InformacaoLog.InserirEndpoint(endpoint.DisplayName);

            _logServico.InformacaoLog.InserirIp(httpContext.Connection.RemoteIpAddress?.ToString())
            .InserirRequestMethod(httpContext.Request.Method)
            .InserirRequestUri(httpContext.GetEndpoint()?.DisplayName)
            .InserirRequestBody(requestBody)
            .InserirResponseBody(responseBody)
            .InserirTraceId(httpContext.TraceIdentifier)
            .InserirUsuario(httpContext.User.Identity.Name);

            _logServico.EscreverLog();
        }

        /// <summary>
        /// Recupera os parametros de rota da aplicação
        /// </summary>
        /// <param name="context"></param>
        private void RecuperarQueryParamsDoRequest(HttpContext context)
        {
            var queryParams = context.GetRouteData();

            if (!queryParams.Values.Any())
                return;

            var values = queryParams.Values.ToList();

            _logServico.InformacaoLog.InserirController($"{values[1].Key} - {values[1].Value}");

            if (values.Count > 2)
            {
                var paramsValue = new StringBuilder();

                for (int i = 2; i < values.Count; i++)
                    paramsValue.AppendLine($"params: {values[i].Key} value: {values[i].Value}");

                _logServico.InformacaoLog.InserirRequestQueryParams(paramsValue.ToString());
            }
        }
    }
}
