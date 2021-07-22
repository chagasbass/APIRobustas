using ApiRobustas.Api.Configuracoes;
using ApiRobustas.Api.Infraestrutura.Autenticacao.Configuracoes;
using ApiRobustas.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.ConfigurarSwagger();
            services.AddMemoryCache();
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiRobustas.Api v1"));
            }

            app.UseCors(x =>
           x.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

            app.UseRequestLogConfiguration();
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();

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
