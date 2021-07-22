using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Dominio.Contextos.Produtos.Comandos;
using ApiRobustas.Dominio.Contextos.Produtos.Repositorios;
using ApiRobustas.Dominio.UnidadeDeTrabalho;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Produtos.Fluxos
{
    /// <summary>
    /// Fluxo para atualização de um produto
    /// </summary>
    public class AtualizarProdutoFluxo : Notifiable<Notification>, IRequestHandler<AtualizarProdutoComando, IComandoResultado>
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IProdutoRepositorio _produtoRepositorio;

        public AtualizarProdutoFluxo(IUnidadeDeTrabalho unidadeDeTrabalho, IProdutoRepositorio produtoRepositorio)
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<IComandoResultado> Handle(AtualizarProdutoComando request, CancellationToken cancellationToken)
        {
            request.ValidarComando();

            if (!request.IsValid)
                return new ComandoResultado(false, "Problemas ao atualizar o produto", request.Notifications);

            var produtoAtualizado = _produtoRepositorio.ListarProduto(request.Id);

            if (produtoAtualizado is null)
            {
                AddNotification("produto", "O Produto não foi encontrado.");
                return new ComandoResultado(false, "Problemas ao atualizar o produto", this.Notifications);
            }

            produtoAtualizado.AlterarPreco(request.Preco)
                             .AlterarQuantidade(request.Quantidade)
                             .AlterarNome(request.Nome)
                             .AlterarDescricao(request.Descricao);

            produtoAtualizado.ValidarEntidade();

            if (!produtoAtualizado.IsValid)
                return new ComandoResultado(false, "Problemas ao atualizar o produto", produtoAtualizado.Notifications);

            _produtoRepositorio.AtualizarProduto(produtoAtualizado);

            await _unidadeDeTrabalho.CommitAsync();

            return new ComandoResultado(true, "Produto Atualizado com sucesso", produtoAtualizado.Id);
        }
    }
}
