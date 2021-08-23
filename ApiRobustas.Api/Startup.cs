using ApiRobustas.Api.Configuracoes;
using ApiRobustas.Api.Infraestrutura.Autenticacao.Configuracoes;
using ApiRobustas.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiRobustas.Api
{
    /// <summary>
    /// Classe de configuração
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.VersionarApi();
            services.ConfigurarSwagger();
            services.AddMemoryCache();
            services.ResolverDependenciasDeLog();
            services.AddGlobalExceptionHandlerMiddleware();
            services.AddHttpClient();
            services.ConfigureOptionPatternObjects(Configuration);
            services.ResolverDependenciasDeContextosDeDados(Configuration);
            services.ResolverDependenciasDaAplicacao();
            services.ComprimirChamadasHtpp();
            services.ConfigurarHttpResponses();
            services.ConfigurarHealthChecks();
            services.AddControllers();
            services.ConfigurarAutenticacao();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseResponseCaching();
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUIMultipleVersions(provider);
            }

            app.UseMiddleware<SerilogRequestLoggerMiddleware>();
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseCors(x =>
           x.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseRouting();

            #region verificar esse cara aqui

            //app.Use(async (context, next) =>
            //{
            //    await next();

            //    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            //    {
            //        var comandoResultado = new ComandoResultado(false, "Acesso negado!");

            //        await context.Response.WriteAsync(JsonSerializer.Serialize(comandoResultado));
            //    }
            //});

            #endregion

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHealthChecks();
            app.UserHealthCheckUi();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/monitor");
            });
        }
    }
}
