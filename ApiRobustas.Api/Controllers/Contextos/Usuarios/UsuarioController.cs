using ApiRobustas.Api.Controllers.Base;
using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Compartilhados.Enumeradores;
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
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class UsuarioController : ApiRobustasController
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
            InserirRotaDefault("v1/usuarios");
        }

        /// <summary>
        /// Efetua o cadastro de um novo usuário
        /// </summary>
        /// <param name="cadastrarUsuarioComando">objeto contendo os dados para cadastro do usuário</param>
        /// <returns>Retorna um objeto do tipo ComandoResultado</returns>
        [HttpPost("")]
        [MapToApiVersion("1.0")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IComandoResultado>> CadastrarUsuarioAsync([FromBody] CadastrarUsuarioComando cadastrarUsuarioComando)
        {
            var comandoResultado = (ComandoResultado)await _mediator.Send(cadastrarUsuarioComando);

            return TratarRequisicao(comandoResultado, EStatusCode.Post);
        }
    }
}
