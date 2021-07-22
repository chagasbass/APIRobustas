using Polly.Retry;

namespace ApiRobustas.Infraestrutura.ServicosExternos.Servicos
{
    public interface IResilienciaServico
    {
        AsyncRetryPolicy RetornarPoliticaDeTratamentoDeRequisicao();
    }
}
