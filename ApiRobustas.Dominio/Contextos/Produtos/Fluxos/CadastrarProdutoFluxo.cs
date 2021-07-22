using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Dominio.Contextos.Categorias.Repositorios;
using ApiRobustas.Dominio.Contextos.Produtos.Comandos;
using ApiRobustas.Dominio.Contextos.Produtos.Entidades;
using ApiRobustas.Dominio.Contextos.Produtos.Repositorios;
using ApiRobustas.Dominio.Contextos.Usuarios.Repositorios;
using ApiRobustas.Dominio.UnidadeDeTrabalho;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Produtos.Fluxos
{
    /// <summary>
    /// Fluxo para cadastro de um novo produto.
    /// </summary>
    public class CadastrarProdutoFluxo : Notifiable<Notification>, IRequestHandler<CadastrarProdutoComando, IComandoResultado>
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public CadastrarProdutoFluxo(IUnidadeDeTrabalho unidadeDeTrabalho,
                                     IProdutoRepositorio produtoRepositorio,
                                     ICategoriaRepositorio categoriaRepositorio,
                                     IUsuarioRepositorio usuarioRepositorio)
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _produtoRepositorio = produtoRepositorio;
            _categoriaRepositorio = categoriaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IComandoResultado> Handle(CadastrarProdutoComando request, CancellationToken cancellationToken)
        {
            /*valida comando
             *verifica se o produto existe
             *verifica se categoria existe 
             *verifica se o usuario existe 
             *salva produto
             *retorna comando resultado.
             */

            request.ValidarComando();

            if (!request.IsValid)
            {
                AddNotifications(request);
                return new ComandoResultado(false, "Problemas ao cadastrar o produto", request.Notifications);
            }

            var produtoJaExiste = _produtoRepositorio.VerificarSeProdutoExiste(request.Nome);

            if (!produtoJaExiste)
            {
                AddNotification("ProdutoId", "O produto já está cadastrado");
                return new ComandoResultado(false, "Problemas ao cadastrar o produto", this.Notifications);
            }

            var categoriaExiste = await _categoriaRepositorio.VerificarSeCategoriaExisteAsync(request.CategoriaId);

            if (!categoriaExiste)
            {
                AddNotification("CategoriaId", "A categoria é inválida.");
                return new ComandoResultado(false, "Problemas ao cadastrar o produto", this.Notifications);
            }

            var usuarioJaExiste = _usuarioRepositorio.VerificarSeUsuarioExiste(request.UsuarioId);

            if (!usuarioJaExiste)
            {
                AddNotification("UsuarioId", "O usuário é inválido.");
                return new ComandoResultado(false, "Problemas ao cadastrar o produto", this.Notifications);
            }

            var novoProduto = new Produto(request.CategoriaId, request.UsuarioId, request.Nome, request.Descricao,
                request.Preco, request.Quantidade);

            await _produtoRepositorio.SalvarProdutoAsync(novoProduto);

            await _unidadeDeTrabalho.CommitAsync();

            return new ComandoResultado(true, "Produto cadastrado com sucesso", novoProduto.Id);
        }
    }
}
