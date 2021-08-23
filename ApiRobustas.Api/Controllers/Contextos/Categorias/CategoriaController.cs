using ApiRobustas.Api.Controllers.Base;
using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Compartilhados.Enumeradores;
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
    public class CategoriaController : ApiRobustasController
    {
        private readonly IMediator _mediator;

        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator;
            InserirRotaDefault("v1/categorias");
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

            return TratarRequisicao(comandoResultado, EStatusCode.Post);
        }
    }
}
