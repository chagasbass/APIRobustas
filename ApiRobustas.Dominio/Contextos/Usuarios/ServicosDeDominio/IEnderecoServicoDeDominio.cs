using ApiRobustas.Dominio.Contextos.Usuarios.Entidades;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Usuarios.ServicosDeDominio
{
    public interface IEnderecoServicoDeDominio
    {
        Task<Endereco> BuscarEnderecoAsync(string cep);
    }
}
