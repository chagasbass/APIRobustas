using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Compartilhados.Enumeradores;
using Microsoft.AspNetCore.Mvc;

namespace ApiRobustas.Api.Controllers.Base
{
    [ApiController]
    public abstract class ApiRobustasController : ControllerBase
    {
        private string _rotaPadrao;

        internal void InserirRotaDefault(string rotaPadrao)
           => _rotaPadrao = rotaPadrao;

        internal ActionResult<IComandoResultado> TratarRequisicao(IComandoResultado comandoResultado, EStatusCode eStatusCode)
        {
            var resultado = (ComandoResultado)comandoResultado;

            if (resultado.Data is null)
            {
                return NotFound(comandoResultado);
            }

            if (!resultado.Sucesso)
            {
                return BadRequest(comandoResultado);
            }

            return TratarRequisicaoDeSucesso(comandoResultado, eStatusCode);
        }

        private ActionResult<IComandoResultado> TratarRequisicaoDeSucesso(IComandoResultado comandoResultado, EStatusCode eStatusCode)
        {
            return eStatusCode switch
            {
                var _ when eStatusCode == EStatusCode.Post => Created(_rotaPadrao, comandoResultado),
                var _ when eStatusCode == EStatusCode.Patch => Ok(comandoResultado),
                var _ when eStatusCode == EStatusCode.Put => NoContent(),
                var _ when eStatusCode == EStatusCode.Get => Ok(comandoResultado),
                var _ when eStatusCode == EStatusCode.Delete => Ok(comandoResultado),
                _ => Ok(comandoResultado),
            };
        }
    }
}
