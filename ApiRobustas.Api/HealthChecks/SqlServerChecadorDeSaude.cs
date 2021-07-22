using ApiRobustas.Compartilhados.Configuracoes;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ApiRobustas.Api.HealthChecks
{
    /// <summary>
    /// Checador de Saúde para Banco de dados Sql Server
    /// </summary>
    public class SqlServerChecadorDeSaude : IHealthCheck
    {
        private readonly ILogger<SqlServerChecadorDeSaude> _logger;
        private readonly ConfiguracoesBaseOptions _configuracoesBaseOptions;

        public SqlServerChecadorDeSaude(ILogger<SqlServerChecadorDeSaude> logger, IOptionsMonitor<ConfiguracoesBaseOptions> options)
        {
            _logger = logger;
            _configuracoesBaseOptions = options.CurrentValue;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            using var conexao = new SqlConnection(_configuracoesBaseOptions.BaseDeDados);

            try
            {
                await conexao.OpenAsync();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy();
            }

            return HealthCheckResult.Healthy();
        }
    }
}
