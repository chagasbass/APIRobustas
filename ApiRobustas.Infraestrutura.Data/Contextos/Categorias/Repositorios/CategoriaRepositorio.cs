using ApiRobustas.Dominio.Contextos.Categorias.Entidades;
using ApiRobustas.Dominio.Contextos.Categorias.Repositorios;
using ApiRobustas.Infraestrutura.Data.ContextosDeDados;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ApiRobustas.Infraestrutura.Data.Contextos.Categorias.Repositorios
{
    /// <summary>
    /// IMplementação do repo de categoria
    /// </summary>
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly ContextoDeDadosEfCore _contexto;

        public CategoriaRepositorio(ContextoDeDadosEfCore contexto)
        {
            _contexto = contexto;
        }

        public async Task SalvarCategoriaAsync(Categoria categoria) => await _contexto.Categorias.AddAsync(categoria);

        public async Task<bool> VerificarSeCategoriaExisteAsync(string nome)
        {
            var categoriaEncontrada = await _contexto.Categorias.FirstOrDefaultAsync(x => x.Nome.ToLower().Equals(nome.ToLower()));
            return categoriaEncontrada is not null;
        }

        public async Task<bool> VerificarSeCategoriaExisteAsync(Guid categoriaId)
        {
            var categoriaEncontrada = await _contexto.Categorias.FirstOrDefaultAsync(c => c.Id == categoriaId);
            return categoriaEncontrada is not null;
        }
    }
}
