using ApiRobustas.Dominio.Contextos.Usuarios.Entidades;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Usuarios.Repositorios
{
    public interface IEnderecoRepositorio
    {
        Endereco BuscarEnderecoPorCep(string cep);
        Task SalvarEnderecoAsync(Endereco endereco);
    }
}