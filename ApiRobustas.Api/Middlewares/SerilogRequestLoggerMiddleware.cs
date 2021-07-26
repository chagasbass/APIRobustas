using ApiRobustas.Logs.Servicos;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
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

        public SerilogRequestLoggerMiddleware(RequestDelegate next, ILogServico logServico)
        {
            _logServico = logServico;

            if (next == null) throw new ArgumentNullException(nameof(next));
            _next = next;
        }

        /// <summary>
        /// Efetua a leitura do HttpContext para recuperar as informações de request e response para os logs
        /// da aplicação.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            //efetuando a leitura do request pois ele vem como um stream.
            string requestBody = await GetRequestBodyAsync(httpContext);

            var originalResponseBodyReference = httpContext.Response.Body;

            using var responseBodyMemoryStream = new MemoryStream();

            var responseBody = await GetResponseBodyAsync(httpContext, responseBodyMemoryStream);

            CreateLogInformation(requestBody, responseBody, httpContext);

            await responseBodyMemoryStream.CopyToAsync(originalResponseBodyReference);
        }

        /// <summary>
        /// Recupera o body recebido na requisição
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private async Task<string> GetRequestBodyAsync(HttpContext httpContext)
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
        private async Task<string> GetResponseBodyAsync(HttpContext httpContext, MemoryStream responseBodyMemoryStream)
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
        private void CreateLogInformation(string requestBody, string responseBody, HttpContext httpContext)
        {
            _logServico.InformacaoLog.Method = httpContext.Request.Method;
            _logServico.InformacaoLog.RequestUri = httpContext.GetEndpoint()?.DisplayName ?? "";
            _logServico.InformacaoLog.RequestBody = requestBody ?? "Requisição sem body";
            _logServico.InformacaoLog.ResponseBody = responseBody;
            _logServico.InformacaoLog.RequestQueryParams = httpContext.Request.Query.ToString();
            _logServico.InformacaoLog.TraceId = httpContext.TraceIdentifier;
            _logServico.InformacaoLog.Usuario = httpContext.User.Identity.Name;

            _logServico.EscreverLog();
        }
    }
}
