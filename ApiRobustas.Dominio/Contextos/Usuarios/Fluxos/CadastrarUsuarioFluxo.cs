using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Dominio.Contextos.Usuarios.Comandos;
using ApiRobustas.Dominio.Contextos.Usuarios.Entidades;
using ApiRobustas.Dominio.Contextos.Usuarios.ObjetosDeValor;
using ApiRobustas.Dominio.Contextos.Usuarios.Repositorios;
using ApiRobustas.Dominio.Contextos.Usuarios.ServicosDeDominio;
using ApiRobustas.Dominio.UnidadeDeTrabalho;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Usuarios.Fluxos
{
    public class CadastrarUsuarioFluxo : Notifiable<Notification>, IRequestHandler<CadastrarUsuarioComando, IComandoResultado>
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IEnderecoServicoDeDominio _enderecoServicoDeDominio;

        public CadastrarUsuarioFluxo(IUnidadeDeTrabalho unidadeDeTrabalho,
                                     IUsuarioRepositorio usuarioRepositorio,
                                     IEnderecoServicoDeDominio enderecoServicoDeDominio)
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _usuarioRepositorio = usuarioRepositorio;
            _enderecoServicoDeDominio = enderecoServicoDeDominio;
        }

        public async Task<IComandoResultado> Handle(CadastrarUsuarioComando request, CancellationToken cancellationToken)
        {
            /*Fail fast Validation
             *verificar se usuario já existe 
             *verificar se cep ja existe
             *se sim pega o endereco e coloca no novo user
             *se nao busca o cep
             *salva o user
             *retorna comandoResultado
             */
            request.ValidarComando();

            if (!request.IsValid)
                return new ComandoResultado(false, "Foram encontrados erros", request.Notifications);

            var usuarioExiste = _usuarioRepositorio.VerificarSeUsuarioExiste(request.Email);

            if (usuarioExiste)
            {
                AddNotification("Usuário", "O usuário já está cadastrado.");
                return new ComandoResultado(false, "Foram encontrados erros", this.Notifications);
            }

            var endereco = await _enderecoServicoDeDominio.BuscarEnderecoAsync(request.Cep);

            var nome = new Nome(request.Nome, request.Sobrenome);

            var novoUsuario = new Usuario(nome, request.Email, request.Senha);
            novoUsuario.ValidarEntidade();

            if (!novoUsuario.IsValid)
                return new ComandoResultado(false, "Foram encontrados erros", novoUsuario.Notifications);

            novoUsuario.AlterarEndereco(endereco);

            await _usuarioRepositorio.SalvarUsuarioAsync(novoUsuario);

            await _unidadeDeTrabalho.CommitAsync();

            return new ComandoResultado(true, "Usuario Cadastrado com sucesso", novoUsuario.Id);
        }
    }
}
