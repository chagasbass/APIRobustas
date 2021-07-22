using ApiRobustas.Compartilhados.Configuracoes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiRobustas.Api.Infraestrutura.Autenticacao.Configuracoes
{
    /// <summary>
    /// COnfigurações para autenticação
    /// </summary>
    public static class AutenticacaoConfig
    {
        public static void ConfigurarAutenticacao(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(ConfiguracoesCompartilhadas.SEGREDO_TOKEN);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
