using ApiRobustas.Dominio.Contextos.Produtos.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRobustas.Infraestrutura.Cache.Servicos
{
    public interface IProdutoMemoriaCacheServico
    {
        Task<IEnumerable<ProdutoQuery>> RecuperarProdutosEmCacheAsync(Guid usuarioId);
        void CriarCacheDeProdutos(IEnumerable<ProdutoQuery> produtos);
    }
}
