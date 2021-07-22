using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Dominio.Contextos.Categorias.Comandos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ApiRobustas.Api.Controllers.Contextos.Categorias
{
    [Route("v1/categorias")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Efetua o cadastro das categorias
        /// </summary>
        [HttpPost("")]
        [MapToApiVersion("1.0")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IComandoResultado>> CriarCategoriaAsync([FromBody] CadastrarCategoriaComando criarCategoriaComando)
        {
            var comandoResultado = await _mediator.Send(criarCategoriaComando);

            return Created("v1/categorias", comandoResultado);
        }
    }
}
