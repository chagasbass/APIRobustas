using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Compartilhados.DetalhesDosProblemas;
using ApiRobustas.Infraestrutura.Logs.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiRobustas.Api.Middlewares
{
    /// <summary>
    /// Middleware de para capturar Exceções Globais
    /// </summary>
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly ILogServico _logServico;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger,
                                                ILogServico logServico)
        {
            _logger = logger;
            _logServico = logServico;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Responsavel por tratar as exceções geradas na aplicação
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            const int statusCode = StatusCodes.Status500InternalServerError;

            var detalhesDoProblema = new DetalhesDoProblema()
            {
                Titulo = "Um erro ocorreu ao processar o request.",
                CodigoHttp = statusCode,
                Detalhe = $"Erro fatal na aplicação,entre em contato com um Desenvolvedor responsável.",
                Instancia = exception.Message
            };

            _logServico.EscreverLogDeErros(exception);

            var comandoResultado = new ComandoResultado(false, "erro na aplicação", detalhesDoProblema);

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = detalhesDoProblema.TipoDeDado;

            await context.Response.WriteAsync(JsonSerializer.Serialize(comandoResultado));
        }
    }
}
