using ApiRobustas.Api.Controllers.Base;
using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Dominio.Contextos.Usuarios.Comandos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ApiRobustas.Api.Controllers.Contextos.Usuarios
{
    /// <summary>
    /// Endpoint de login
    /// </summary>
    [Route("v1/login")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class LoginController : ApiRobustasController
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Efetua o login do usuário
        /// </summary>
        /// <param name="efetuarLoginComando">objeto contendo os dados para login</param>
        /// <returns>Retorna um objeto do tipo ComandoResultado</returns>
        [HttpPost("")]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IComandoResultado>> EfetuarLoginAsync([FromBody] EfetuarLoginComando efetuarLoginComando)
        {
            var comandoResultado = (ComandoResultado)await _mediator.Send(efetuarLoginComando);

            if (!comandoResultado.Sucesso)
                return Unauthorized(comandoResultado);

            return Ok(comandoResultado);
        }
    }
}
