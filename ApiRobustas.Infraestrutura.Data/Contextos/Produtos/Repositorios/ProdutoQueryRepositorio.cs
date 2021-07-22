using ApiRobustas.Dominio.Contextos.Produtos.Queries;
using ApiRobustas.Dominio.Contextos.Produtos.Repositorios;
using ApiRobustas.Infraestrutura.Data.ContextosDeDados;
using ApiRobustas.Infraestrutura.Data.QueryHelpers;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRobustas.Infraestrutura.Data.Contextos.Produtos.Repositorios
{
    public class ProdutoQueryRepositorio : IProdutoQueryRepositorio
    {
        private readonly ContextoDeDadosDapper _Contexto;

        public ProdutoQueryRepositorio(ContextoDeDadosDapper contexto)
        {
            _Contexto = contexto;
        }

        public async Task<IEnumerable<ProdutoQuery>> ListarProdutosAsync(Guid usuarioId)
        {
            object filtroConsulta = new { usuarioId = usuarioId };

            var produtos = await _Contexto.Conexao.QueryAsync<ProdutoQuery>(
                                                              ProdutoQueryHelper.ListarProdutos(),
                                                              filtroConsulta);

            return produtos;
        }

        public async Task<IEnumerable<ProdutoQuery>> ListarProdutosPorCategoriaAsync(Guid categoriaId)
        {
            object filtroConsulta = new { categoriaId = categoriaId };

            var produtos = await _Contexto.Conexao.QueryAsync<ProdutoQuery>(
                                                              ProdutoQueryHelper.ListarProdutosPorCategoria(),
                                                              filtroConsulta);

            return produtos;
        }

        public async Task<IEnumerable<ProdutoQuery>> ListarProdutosPorIdAsync(Guid produtoId)
        {
            object filtroConsulta = new { produtoId = produtoId };

            var produtos = await _Contexto.Conexao.QueryAsync<ProdutoQuery>(
                                                              ProdutoQueryHelper.ListarProdutosPorId(),
                                                              filtroConsulta);

            return produtos;
        }
    }
}
