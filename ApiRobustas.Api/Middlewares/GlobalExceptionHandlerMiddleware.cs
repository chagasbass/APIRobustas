using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Compartilhados.DetalhesDosProblemas;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiRobustas.Api.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro inesperado: {ex}");

                await HandleExceptionAsync(context, ex);
            }
        }

        public Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            const int statusCode = StatusCodes.Status500InternalServerError;

            var detalhesDoProblema = new DetalhesDoProblema()
            {
                Titulo = "Um erro ocorreu ao processar o request.",
                CodigoHttp = statusCode,
                Detalhe = $"Erro fatal na aplicação,entre em contato com um Desenvolvedor responsável.",
                Instancia = exception.Message
            };

            var logDeErro = new DetalhesDoProblema()
            {
                Titulo = "Erro Inesperado",
                CodigoHttp = statusCode,
                Detalhe = $"StackTrace:{exception.StackTrace} - Inner - {exception.InnerException}",
                Instancia = exception.Message
            };

            _logger.LogInformation($"Erro na aplicação", logDeErro);

            var comandoResultado = new ComandoResultado(false, "erro na aplicação", detalhesDoProblema);

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = detalhesDoProblema.TipoDeDado;

            return context.Response.WriteAsync(JsonSerializer.Serialize(comandoResultado));
        }
    }
}
