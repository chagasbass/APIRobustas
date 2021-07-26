using System.Text;

namespace ApiRobustas.Infraestrutura.Data.QueryHelpers
{
    public static class CategoriaQueryHelper
    {
        public static string ListarCategorias()
        {
            var query = new StringBuilder();
            query.AppendLine(" SELECT ID, NOME FROM CATEGORIA ORDER BY NOME ASC");

            return query.ToString();
        }

        public static string ListarCategoriasPorId()
        {
            var query = new StringBuilder();
            query.AppendLine(" SELECT ID, NOME FROM CATEGORIA");
            query.AppendLine(" WHERE ID = @id");

            return query.ToString();
        }
    }
}
