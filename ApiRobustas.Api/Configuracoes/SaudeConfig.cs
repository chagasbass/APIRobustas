using ApiRobustas.Api.HealthChecks;
using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Compartilhados.Saude;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Mime;
using System.Text.Json;

namespace ApiRobustas.Api.Configuracoes
{
    /// <summary>
    /// Configuração da Saúde da aplicação
    /// </summary>
    public static class SaudeConfig
    {
        public static void ConfigurarHealthChecks(this IServiceCollection services)
        {
            #region healthchecks customizados
            services.AddHealthChecks()
           .AddCheck<SqlServerChecadorDeSaude>("Banco de Dados")
           .AddCheck<ServicoExternoChegadorDeSaude>("Serviço de CEP");
            #endregion

            #region healthcheckUI
            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.SetEvaluationTimeInSeconds(60);
                setup.MaximumHistoryEntriesPerEndpoint(50);
            }).AddInMemoryStorage();
            #endregion
        }

        public static void UseHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/app-status");
            app.UseHealthChecks("/app-status-json",
                new HealthCheckOptions()
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonSerializer.Serialize(
                            new ComandoResultado()
                            {
                                Sucesso = true,
                                Mensagem = "Status da aplicação",
                                Data = new InformacaoDeSaude()
                                {
                                    Nome = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                                    Versao = "V1",
                                    Data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                    Status = report.Status.ToString(),
                                }
                            });

                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
        }

        public static void UserHealthCheckUi(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/healthchecks-data-ui", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Ativa o dashboard para a visualização da situação de cada Health Check
            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/monitor";
            });
        }
    }
}
