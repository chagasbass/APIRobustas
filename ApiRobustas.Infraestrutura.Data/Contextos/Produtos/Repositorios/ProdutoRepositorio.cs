using ApiRobustas.Dominio.Contextos.Produtos.Entidades;
using ApiRobustas.Dominio.Contextos.Produtos.Repositorios;
using ApiRobustas.Infraestrutura.Data.ContextosDeDados;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRobustas.Infraestrutura.Data.Contextos.Produtos.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly ContextoDeDadosEfCore _Contexto;

        public ProdutoRepositorio(ContextoDeDadosEfCore contexto)
        {
            _Contexto = contexto;
        }

        public void AtualizarProduto(Produto produto) => _Contexto.Produtos.Update(produto);

        public void ExcluirProduto(Produto produto) => _Contexto.Produtos.Remove(produto);

        public Produto ListarProduto(Guid id) => _Contexto.Produtos.FirstOrDefault(p => p.Id == id);

        public async Task SalvarProdutoAsync(Produto produto) => await _Contexto.Produtos.AddRangeAsync(produto);

        public bool VerificarSeProdutoExiste(string nome)
        {
            var produtoEncontrado = _Contexto.Produtos.FirstOrDefault(p => p.Nome.ToLower().Equals(nome.ToLower()));
            return produtoEncontrado is null;
        }
    }
}
