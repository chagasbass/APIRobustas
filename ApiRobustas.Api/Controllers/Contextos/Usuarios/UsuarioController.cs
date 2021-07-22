using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Dominio.Contextos.Usuarios.Comandos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ApiRobustas.Api.Controllers.Contextos.Usuarios
{
    /// <summary>
    /// Representa o Endpoint de usuários
    /// </summary>
    [Route("v1/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Efetua o cadastro de um novo usuário
        /// </summary>
        /// <param name="cadastrarUsuarioComando">objeto contendo os dados para cadastro do usuário</param>
        /// <returns>Retorna um objeto do tipo ComandoResultado</returns>
        [HttpPost("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IComandoResultado>> CadastrarUsuarioAsync([FromBody] CadastrarUsuarioComando cadastrarUsuarioComando)
        {
            var comandoResultado = (ComandoResultado)await _mediator.Send(cadastrarUsuarioComando);

            if (!comandoResultado.Sucesso)
                return BadRequest(comandoResultado);

            return Created("v1/usuarios", comandoResultado);
        }
    }
}
