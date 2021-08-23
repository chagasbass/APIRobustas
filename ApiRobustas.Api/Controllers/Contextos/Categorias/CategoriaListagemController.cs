using ApiRobustas.Api.Controllers.Base;
using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Compartilhados.Enumeradores;
using ApiRobustas.Dominio.Contextos.Categorias.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ApiRobustas.Api.Controllers.Contextos.Categorias
{
    /// <summary>
    /// Controller de listagem para categorias
    /// </summary>
    [ApiController]
    [Route("v1/categorias")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CategoriaListagemController : ApiRobustasController
    {
        /// <summary>
        /// Efetua a listagem das categorias
        /// </summary>
        [HttpGet("")]
        [MapToApiVersion("1.0")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IComandoResultado>> ListarCategoriasAsync([FromServices] ICategoriaQueryRepositorio _categoriaQueryRepositorio)
        {
            var comandoResultado = new ComandoResultado
            {
                Data = await _categoriaQueryRepositorio.ListarCategoriasAsync()
            };

            return TratarRequisicao(comandoResultado, EStatusCode.Get);
        }

        /// <summary>
        /// Efetua a listagem das categorias por um id Informado
        /// </summary> 
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ComandoResultado), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IComandoResultado>> ListarCategoriasAsync([FromServices] ICategoriaQueryRepositorio _categoriaQueryRepositorio,
                                                                                 [FromRoute] Guid id)
        {
            var comandoResultado = new ComandoResultado
            {
                Data = await _categoriaQueryRepositorio.ListarCategoriasAsync(id)
            };

            return TratarRequisicao(comandoResultado, EStatusCode.Get);
        }
    }
}
