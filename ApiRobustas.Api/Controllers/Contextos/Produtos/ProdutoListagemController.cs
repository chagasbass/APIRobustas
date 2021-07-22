using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Dominio.Contextos.Produtos.Repositorios;
using ApiRobustas.Infraestrutura.Cache.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ApiRobustas.Api.Controllers.Contextos.Produtos
{
    /// <summary>
    /// Controller de listagem de produtos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoListagemController : ControllerBase
    {
        private readonly IProdutoMemoriaCacheServico _produtoMemoriaCacheServico;
        private readonly IProdutoQueryRepositorio _produtoQueryRepositorio;

        public ProdutoListagemController(IProdutoMemoriaCacheServico produtoMemoriaCacheServico,
                                         IProdutoQueryRepositorio produtoQueryRepositorio)
        {
            _produtoMemoriaCacheServico = produtoMemoriaCacheServico;
            _produtoQueryRepositorio = produtoQueryRepositorio;
        }

        /// <summary>
        /// Efetua a listagem dos Produtos usando Cache
        /// </summary>
        [HttpGet("{usuarioId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IComandoResultado>> ListarProdutosAsync([FromRoute] Guid usuarioId)
        {
            var comandoResultado = new ComandoResultado
            {
                Data = await _produtoMemoriaCacheServico.RecuperarProdutosEmCacheAsync(usuarioId)
            };

            if (comandoResultado.Data is null)
                return NotFound();

            return Ok(comandoResultado);
        }

        /// <summary>
        /// Efetua a listagem dos Produtos por categoria
        /// </summary>
        [HttpGet("{categoriaId}/categoria")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IComandoResultado>> ListarProdutoPorCategoriasAsync([FromRoute] Guid categoriaId)
        {
            var comandoResultado = new ComandoResultado
            {
                Data = await _produtoQueryRepositorio.ListarProdutosPorCategoriaAsync(categoriaId)
            };

            if (comandoResultado.Data is null)
                return NotFound();

            return Ok(comandoResultado);
        }
    }
}
