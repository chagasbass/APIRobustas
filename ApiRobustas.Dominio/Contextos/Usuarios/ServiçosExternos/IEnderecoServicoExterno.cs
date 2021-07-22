using ApiRobustas.Dominio.Contextos.Usuarios.Queries;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Usuarios.ServiçosExternos
{
    public interface IEnderecoServicoExterno
    {
        Task<EnderecoQuery> BuscarEnderecoPorCepAsync(string cep);
    }
}
