using System.Threading.Tasks;

namespace ApiRobustas.Dominio.UnidadeDeTrabalho
{
    public interface IUnidadeDeTrabalho
    {
        Task CommitWithTransactionAsync();
        Task RollbackWithTransactionAsync();
        Task CommitAsync();
        Task CriarTransacaoAsync();
    }
}
