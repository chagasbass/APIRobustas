using ApiRobustas.Dominio.Contextos.Categorias.Entidades;
using System;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Categorias.Repositorios
{
    public interface ICategoriaRepositorio
    {
        Task SalvarCategoriaAsync(Categoria categoria);
        Task<bool> VerificarSeCategoriaExisteAsync(string nome);
        Task<bool> VerificarSeCategoriaExisteAsync(Guid categoriaId);
    }
}