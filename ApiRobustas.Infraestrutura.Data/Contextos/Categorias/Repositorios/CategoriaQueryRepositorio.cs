using ApiRobustas.Dominio.Contextos.Categorias.Queries;
using ApiRobustas.Dominio.Contextos.Categorias.Repositorios;
using ApiRobustas.Infraestrutura.Data.ContextosDeDados;
using ApiRobustas.Infraestrutura.Data.QueryHelpers;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRobustas.Infraestrutura.Data.Contextos.Categorias.Repositorios
{
    public class CategoriaQueryRepositorio : ICategoriaQueryRepositorio
    {
        private readonly ContextoDeDadosDapper _contexto;

        public CategoriaQueryRepositorio(ContextoDeDadosDapper contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<CategoriaQuery>> ListarCategoriasAsync()
        {
            var categorias = await _contexto.Conexao.QueryAsync<CategoriaQuery>(
                                                             CategoriaQueryHelper.ListarCategorias());

            return categorias;
        }

        public async Task<CategoriaQuery> ListarCategoriasAsync(Guid id)
        {
            object filtroConsulta = new { id = id };

            var categoria = await _contexto.Conexao.QueryFirstOrDefaultAsync<CategoriaQuery>(
                                                              CategoriaQueryHelper.ListarCategoriasPorId(),
                                                              filtroConsulta);

            return categoria;
        }
    }
}
