using ApiRobustas.Compartilhados.Configuracoes;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiRobustas.Api.HealthChecks
{
    /// <summary>
    /// Checador De Saúde para serviço externo de cep
    /// </summary>
    public class ServicoExternoChegadorDeSaude : IHealthCheck
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ConfiguracoesBaseOptions _configuracoesBaseOptions;

        private const string CEP = "24040110";

        public ServicoExternoChegadorDeSaude(IHttpClientFactory clientFactory,
                                             IOptionsMonitor<ConfiguracoesBaseOptions> options)
        {
            _clientFactory = clientFactory;
            _configuracoesBaseOptions = options.CurrentValue;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var url = _configuracoesBaseOptions.ServicoCep.Replace("meu_cep", CEP);
            using var clienteHttp = _clientFactory.CreateClient();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await clienteHttp.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return HealthCheckResult.Unhealthy();
            }
            catch
            {
                return HealthCheckResult.Unhealthy();
            }

            return HealthCheckResult.Healthy();
        }
    }
}
