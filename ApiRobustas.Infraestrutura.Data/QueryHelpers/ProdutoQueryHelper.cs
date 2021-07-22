using System.Text;

namespace ApiRobustas.Infraestrutura.Data.QueryHelpers
{
    public static class ProdutoQueryHelper
    {
        public static string ListarProdutos()
        {
            var query = new StringBuilder();
            query.AppendLine(" SELECT  p.[ID], p.[NOME], p.[DESCRICAO],c.[NOME] as Categoria");
            query.AppendLine(" p.[PRECO],p.[QUANTIDADE]");
            query.AppendLine(" FROM[apirobustas].[dbo].[PRODUTO] p JOIN CATEGORIA c ON p.[id_Categoria] = c.[ID]");
            query.AppendLine(" WHERE p.[ID_USUARIO] = @usuarioId");

            return query.ToString();
        }

        public static string ListarProdutosPorId()
        {
            var query = new StringBuilder();
            query.AppendLine(" SELECT  p.[ID], p.[NOME], p.[DESCRICAO],c.[NOME] as Categoria");
            query.AppendLine(" p.[PRECO],p.[QUANTIDADE]");
            query.AppendLine(" FROM[apirobustas].[dbo].[PRODUTO] p JOIN CATEGORIA c ON p.[id_Categoria] = c.[ID]");
            query.AppendLine(" WHERE p.[ID] = @produtoId");

            return query.ToString();
        }

        public static string ListarProdutosPorCategoria()
        {
            var query = new StringBuilder();
            query.AppendLine(" SELECT  p.[ID], p.[NOME], p.[DESCRICAO],c.[NOME] as Categoria,");
            query.AppendLine(" p.[PRECO],p.[QUANTIDADE]");
            query.AppendLine(" FROM PRODUTO p JOIN CATEGORIA c ON p.[id_Categoria] = c.[ID]");
            query.AppendLine(" WHERE c.[ID] = @categoriaId");
            query.AppendLine(" ORDER BY p.[NOME]");

            return query.ToString();
        }
    }
}
