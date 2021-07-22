using ApiRobustas.Dominio.Contextos.Categorias.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Categorias.Repositorios
{
    public interface ICategoriaQueryRepositorio
    {
        Task<IEnumerable<CategoriaQuery>> ListarCategoriasAsync();
        Task<CategoriaQuery> ListarCategoriasAsync(Guid id);
    }
}
