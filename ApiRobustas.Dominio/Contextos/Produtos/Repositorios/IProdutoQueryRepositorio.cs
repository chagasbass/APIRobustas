using ApiRobustas.Dominio.Contextos.Produtos.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Produtos.Repositorios
{
    public interface IProdutoQueryRepositorio
    {
        Task<IEnumerable<ProdutoQuery>> ListarProdutosPorCategoriaAsync(Guid categoriaId);
        Task<IEnumerable<ProdutoQuery>> ListarProdutosAsync(Guid usuarioId);
        Task<IEnumerable<ProdutoQuery>> ListarProdutosPorIdAsync(Guid produtoId);
    }
}
