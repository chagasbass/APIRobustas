using ApiRobustas.Compartilhados.Configuracoes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiRobustas.Api.Configuracoes
{
    /// <summary>
    /// Extensão para usar o Options Pattern
    /// </summary>
    public static class OptionPatternConfig
    {
        /// <summary>
        /// Resolve o options pattern
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureOptionPatternObjects(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfiguracoesBaseOptions>(configuration.GetSection(ConfiguracoesBaseOptions.ConfiguracoesBase));
            services.Configure<ConfiguracoesDeCacheOptions>(configuration.GetSection(ConfiguracoesDeCacheOptions.ConfiguracoesBase));
            services.Configure<ConfiguracoesResilienciaOpcaoOptions>(configuration.GetSection(ConfiguracoesResilienciaOpcaoOptions.ConfiguracoesBase));
        }
    }
}
