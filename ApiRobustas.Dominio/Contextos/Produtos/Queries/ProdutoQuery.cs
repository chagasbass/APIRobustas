using System;

namespace ApiRobustas.Dominio.Contextos.Produtos.Queries
{
    public class ProdutoQuery
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

        public ProdutoQuery() { }

    }
}