using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Dominio.Contextos.Categorias.Comandos;
using ApiRobustas.Dominio.Contextos.Categorias.Entidades;
using ApiRobustas.Dominio.Contextos.Categorias.Repositorios;
using ApiRobustas.Dominio.UnidadeDeTrabalho;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Categorias.Fluxos
{
    public class CadastrarCategoriaFluxo : Notifiable<Notification>, IRequestHandler<CadastrarCategoriaComando, IComandoResultado>
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

        public CadastrarCategoriaFluxo(IUnidadeDeTrabalho unidadeDeTrabalho, ICategoriaRepositorio categoriaRepositorio)
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _categoriaRepositorio = categoriaRepositorio;
        }

        public async Task<IComandoResultado> Handle(CadastrarCategoriaComando request, CancellationToken cancellationToken)
        {
            request.ValidarComando();

            if (!request.IsValid)
            {
                AddNotifications(request);
                return new ComandoResultado(false, "Problemas ao cadastrar a categoria", request.Notifications);
            }

            var categoriaExiste = await _categoriaRepositorio.VerificarSeCategoriaExisteAsync(request.Nome);

            if (categoriaExiste)
            {
                AddNotification("Categoria", "A categoria ja existe.");
                return new ComandoResultado(false, "Problemas ao cadastrar a categoria", this.Notifications);
            }

            var novaCategoria = new Categoria(request.Nome, request.Descricao);

            await _categoriaRepositorio.SalvarCategoriaAsync(novaCategoria);
            await _unidadeDeTrabalho.CommitAsync();

            return new ComandoResultado(true, "Categoria cadastrada com sucesso", new { Id = novaCategoria.Id });
        }
    }
}
