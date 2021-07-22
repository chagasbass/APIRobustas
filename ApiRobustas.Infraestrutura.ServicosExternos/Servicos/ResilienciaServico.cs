using ApiRobustas.Compartilhados.Configuracoes;
using ApiRobustas.Infraestrutura.ServicosExternos.Servicos;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System;
using System.Net.Http;

namespace ApiRobustas.Infraestrutura.ServicosExternos.Configuracoes
{
    public class ResilienciaServico : IResilienciaServico
    {
        private readonly ConfiguracoesResilienciaOpcaoOptions _configuracoesResilienciaOpcao;

        public ResilienciaServico(IOptionsMonitor<ConfiguracoesResilienciaOpcaoOptions> opcoes)
        {
            _configuracoesResilienciaOpcao = opcoes.CurrentValue;
        }

        public AsyncRetryPolicy RetornarPoliticaDeTratamentoDeRequisicao()
        {
            var politicaDeTratamentoDeRequisicao = Policy
                       .Handle<HttpRequestException>()
                       .WaitAndRetryAsync(_configuracoesResilienciaOpcao.QuantidadeDeTentativas,
                                         i => TimeSpan.FromMilliseconds(_configuracoesResilienciaOpcao.PausaEntreAsFalhas));

            return politicaDeTratamentoDeRequisicao;
        }
    }
}
