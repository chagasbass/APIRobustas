using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Compartilhados.ValidacoesDeDominio;
using ApiRobustas.Dominio.Contextos.Usuarios.Comandos;
using ApiRobustas.Dominio.Contextos.Usuarios.Queries;
using ApiRobustas.Dominio.Contextos.Usuarios.Repositorios;
using ApiRobustas.Dominio.Contextos.Usuarios.ServiçosExternos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Usuarios.Fluxos
{
    /// <summary>
    /// Fluxo para efetuar o login.
    /// </summary>
    public class EfetuarLoginFluxo : IRequestHandler<EfetuarLoginComando, IComandoResultado>
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ITokenServico _tokenServico;

        public EfetuarLoginFluxo(IUsuarioRepositorio usuarioRepositorio, ITokenServico tokenServico)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _tokenServico = tokenServico;
        }

        public async Task<IComandoResultado> Handle(EfetuarLoginComando request, CancellationToken cancellationToken)
        {
            /*verifica se comando é válido
             *encripta senha 
             *Valida o login do usuario 
             * gera o token
             * retorna comando resultado.
             */

            if (!request.IsValid)
                return new ComandoResultado(false, "Problemas ao efetuar o login", request.Notifications);

            var senhaEncriptada = ValidacoesCustomizadas.EncriptarSenha(request.Senha);

            var usuarioFoiEncontrado = _usuarioRepositorio.ValidarLogin(request.Email, senhaEncriptada);

            if (!usuarioFoiEncontrado)
                return new ComandoResultado(false, "Usuário Inválido.");

            var token = _tokenServico.GerarToken(request);

            var loginQuery = new LoginQuery
            {
                Email = request.Email,
                Token = token
            };

            return new ComandoResultado
            {
                Sucesso = true,
                Data = loginQuery
            };
        }
    }
}
