using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ApiRobustas.Api.Configuracoes
{
    /// <summary>
    /// Extensão para configuração de Swagger
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Contém as configurações do swagger para
        /// Criar versões diferentes de rotas
        /// Add comentários aos endpoints
        /// Inserir autenticação bearer no swagger
        /// resolver conflitos de rotas
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigurarSwagger(this IServiceCollection services)
        {
            #region Criar versões diferentes de rotas
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ApiRobustas Versão 1",
                    Version = "v1"
                });

                //c.SwaggerDoc("v2", new OpenApiInfo
                //{
                //    Version = "v2",
                //    Title = "API Robustas Versão 2",
                //});
                #endregion

                #region Resolver conflitos de rotas
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                #endregion

                #region Inserindo Autenticação Bearer no swagger
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Autorização efetuada via JWT token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                c.AddSecurityRequirement(securityRequirement);

                #endregion

                #region Add comentários aos endpoints
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
                #endregion
            });
        }

        /// <summary>
        /// Extensão para configurar rotas iguais mas com versões diferentes
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        public static void UseSwaggerUIMultipleVersions(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}
