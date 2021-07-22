using ApiRobustas.Dominio.Contextos.Produtos.Entidades;
using System;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Produtos.Repositorios
{
    public interface IProdutoRepositorio
    {
        Task SalvarProdutoAsync(Produto produto);
        void AtualizarProduto(Produto produto);
        void ExcluirProduto(Produto produto);
        bool VerificarSeProdutoExiste(string nome);
        Produto ListarProduto(Guid id);
    }
}
