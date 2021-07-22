using ApiRobustas.Compartilhados.Configuracoes;
using ApiRobustas.Dominio.Contextos.Usuarios.Queries;
using ApiRobustas.Dominio.Contextos.Usuarios.ServiçosExternos;
using ApiRobustas.Infraestrutura.ServicosExternos.Servicos;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiRobustas.Infraestrutura.ServicosExternos.Externos
{
    /// <summary>
    /// Implementação do serviço externo de endereço.
    /// </summary>
    public class EnderecoServicoExterno : IEnderecoServicoExterno
    {
        private readonly ConfiguracoesBaseOptions _configuracoesBaseOpcao;
        private readonly IHttpClientFactory _ClienteHttp;
        private readonly IResilienciaServico _resilienciaServico;

        public EnderecoServicoExterno(IHttpClientFactory clienteHttp,
                                      IResilienciaServico resilienciaServico,
                                      IOptionsMonitor<ConfiguracoesBaseOptions> opcoes)
        {
            _configuracoesBaseOpcao = opcoes.CurrentValue;
            _ClienteHttp = clienteHttp;
            _resilienciaServico = resilienciaServico;
        }

        private HttpRequestMessage MontarRequisicao(string cep)
        {
            //viacep.com.br/ws/24130110/json/
            var cepTratado = cep.Replace("-", string.Empty);
            var url = _configuracoesBaseOpcao.ServicoCep.Replace("meu_cep", cepTratado);

            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        public async Task<EnderecoQuery> BuscarEnderecoPorCepAsync(string cep)
        {
            var clienteExterno = _ClienteHttp.CreateClient();

            var requisicao = MontarRequisicao(cep);

            var endereco = new EnderecoQuery();

            var configuracoesDeResiliencia = _resilienciaServico.RetornarPoliticaDeTratamentoDeRequisicao();

            await configuracoesDeResiliencia.ExecuteAsync(async () =>
            {
                var resposta = await clienteExterno.SendAsync(requisicao);

                if (resposta.IsSuccessStatusCode)
                {
                    var retorno = await resposta.Content.ReadAsStringAsync();
                    endereco = JsonSerializer.Deserialize<EnderecoQuery>(retorno);
                }
            });

            return endereco;
        }
    }
}
