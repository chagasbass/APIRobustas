using ApiRobustas.Dominio.Contextos.Usuarios.Entidades;
using ApiRobustas.Dominio.Contextos.Usuarios.Repositorios;
using ApiRobustas.Dominio.Contextos.Usuarios.ServiçosExternos;
using ApiRobustas.Dominio.UnidadeDeTrabalho;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Usuarios.ServicosDeDominio
{
    public class EnderecoServicoDeDominio : IEnderecoServicoDeDominio
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IEnderecoRepositorio _enderecoRepositorio;
        private readonly IEnderecoServicoExterno _enderecoServicoExterno;

        public EnderecoServicoDeDominio(IUnidadeDeTrabalho unidadeDeTrabalho,
                                        IEnderecoRepositorio enderecoRepositorio,
                                        IEnderecoServicoExterno enderecoServicoExterno)
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _enderecoRepositorio = enderecoRepositorio;
            _enderecoServicoExterno = enderecoServicoExterno;
        }

        public async Task<Endereco> BuscarEnderecoAsync(string cep)
        {
            var enderecoCadastradoNaBase = _enderecoRepositorio.BuscarEnderecoPorCep(cep);

            if (enderecoCadastradoNaBase is not null)
                return enderecoCadastradoNaBase;

            var enderecoExterno = await _enderecoServicoExterno.BuscarEnderecoPorCepAsync(cep);

            if (string.IsNullOrEmpty(enderecoExterno.Cep))
                return default;

            var novoEndereco = new Endereco(enderecoExterno.Cep, enderecoExterno.Logradouro, enderecoExterno.Bairro,
               enderecoExterno.Localidade, enderecoExterno.Uf);

            await _enderecoRepositorio.SalvarEnderecoAsync(novoEndereco);

            await _unidadeDeTrabalho.CommitAsync();

            return novoEndereco;
        }
    }
}
