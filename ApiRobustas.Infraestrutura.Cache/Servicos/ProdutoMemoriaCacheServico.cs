using ApiRobustas.Compartilhados.Configuracoes;
using ApiRobustas.Dominio.Contextos.Produtos.Queries;
using ApiRobustas.Dominio.Contextos.Produtos.Repositorios;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRobustas.Infraestrutura.Cache.Servicos
{
    public class ProdutoMemoriaCacheServico : IProdutoMemoriaCacheServico
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IProdutoQueryRepositorio _produtoQueryRepositorio;
        private readonly ConfiguracoesDeCacheOptions _configuracoesDeCache;

        public ProdutoMemoriaCacheServico(IMemoryCache memoryCache,
                                          IProdutoQueryRepositorio produtoQueryRepositorio,
                                          IOptionsMonitor<ConfiguracoesDeCacheOptions> opcoes)
        {
            _memoryCache = memoryCache;
            _produtoQueryRepositorio = produtoQueryRepositorio;
            _configuracoesDeCache = opcoes.CurrentValue;
        }

        public void CriarCacheDeProdutos(IEnumerable<ProdutoQuery> produtos)
        {
            var memoryCacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_configuracoesDeCache.TempoDeExpiracaoRelativo),
                SlidingExpiration = TimeSpan.FromSeconds(_configuracoesDeCache.TempoOcioso) //se nao for acessado , é retirado da memoria
            };

            _memoryCache.Set(_configuracoesDeCache.ChaveProdutoCache, produtos, memoryCacheEntryOptions);
        }

        public async Task<IEnumerable<ProdutoQuery>> RecuperarProdutosEmCacheAsync(Guid usuarioId)
        {
            if (_memoryCache.TryGetValue(_configuracoesDeCache.ChaveProdutoCache, out List<ProdutoQuery> produtos))
                return produtos;

            var novosProdutos = await _produtoQueryRepositorio.ListarProdutosAsync(usuarioId);

            CriarCacheDeProdutos(novosProdutos);

            return novosProdutos;
        }
    }
}
