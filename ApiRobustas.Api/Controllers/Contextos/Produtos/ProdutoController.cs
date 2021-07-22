using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Dominio.Contextos.Produtos.Comandos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ApiRobustas.Api.Controllers.Contextos.Produtos
{
    /// <summary>
    /// COntroller de Produtos
    /// </summary>
    [Route("v1/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProdutoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Efetua o cadastro de um produto
        /// </summary>
        /// <param name="cadastrarProdutoComando">objeto contendo os dados do produto</param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IComandoResultado>> CadastrarProdutoAsync([FromBody] CadastrarProdutoComando cadastrarProdutoComando)
        {
            var comandoResultado = (ComandoResultado)await _mediator.Send(cadastrarProdutoComando);

            if (!comandoResultado.Sucesso)
                return BadRequest(comandoResultado);

            return Created("v1/produtos", comandoResultado);
        }

        /// <summary>
        /// Efetua a atualização de um produto
        /// </summary>
        /// <param name="atualizarProdutoComando">objeto que contém as informações para atualizar um produto</param>
        /// <returns>retorna um objeto do tipo ComandoResultado</returns>
        //[Authorize]
        [HttpPut("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IComandoResultado>> AtualizarProdutoAsync([FromBody] AtualizarProdutoComando atualizarProdutoComando)
        {
            var comandoResultado = (ComandoResultado)await _mediator.Send(atualizarProdutoComando);

            if (!comandoResultado.Sucesso)
                return BadRequest(comandoResultado);

            return NoContent();
        }
    }
}
