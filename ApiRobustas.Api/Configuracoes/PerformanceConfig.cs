using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;
using System.Linq;

namespace ApiRobustas.Api.Configuracoes
{
    ///// <summary>
    /// Extensão de performance para compressão de requisições
    /// </summary>
    public static class PerformanceConfig
    {
        public static void ComprimirChamadasHtpp(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;

                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            services.Configure<BrotliCompressionProviderOptions>(brotliOptions =>
            {
                brotliOptions.Level = CompressionLevel.Fastest;
            });

            services.Configure<GzipCompressionProviderOptions>(gzipOptions =>
            {
                gzipOptions.Level = CompressionLevel.Fastest;
            });
        }

        public static void ConfigurarHttpResponses(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(opcoes =>
            {
                var serializerOptions = opcoes.JsonSerializerOptions;
                serializerOptions.IgnoreNullValues = true;
                serializerOptions.IgnoreReadOnlyProperties = true;
                serializerOptions.WriteIndented = true;
            });
        }
    }
}