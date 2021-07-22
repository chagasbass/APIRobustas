using ApiRobustas.Dominio.Contextos.Usuarios.Entidades;
using ApiRobustas.Dominio.Contextos.Usuarios.Repositorios;
using ApiRobustas.Infraestrutura.Data.ContextosDeDados;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRobustas.Infraestrutura.Data.Contextos.Usuarios.Repositorios
{
    public class EnderecoRepositorio : IEnderecoRepositorio
    {
        private readonly ContextoDeDadosEfCore _Contexto;

        public EnderecoRepositorio(ContextoDeDadosEfCore contexto)
        {
            _Contexto = contexto;
        }

        public Endereco BuscarEnderecoPorCep(string cep) => _Contexto.Enderecos.FirstOrDefault(x => x.CEP == cep);

        public async Task SalvarEnderecoAsync(Endereco endereco) => await _Contexto.Enderecos.AddAsync(endereco);
    }
}
