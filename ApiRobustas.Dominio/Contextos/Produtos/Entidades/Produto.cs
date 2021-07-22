using ApiRobustas.Compartilhados.EntidadesBase;
using ApiRobustas.Compartilhados.ValidacoesDeDominio;
using ApiRobustas.Dominio.Contextos.Categorias.Entidades;
using ApiRobustas.Dominio.Contextos.Usuarios.Entidades;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace ApiRobustas.Dominio.Contextos.Produtos.Entidades
{
    public class Produto : Entidade, IValidacaoEntidade
    {
        public Guid CategoriaId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }
        public Categoria Categoria { get; private set; }
        public Usuario Usuario { get; private set; }

        protected Produto() { }

        public Produto(Guid categoriaId, Guid usuarioId, string nome,
                       string descricao, decimal preco, int quantidade)
        {
            CategoriaId = categoriaId;
            UsuarioId = usuarioId;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Quantidade = quantidade;

            ValidarEntidade();
        }

        #region Chain Methods
        public Produto AlterarNome(string nome)
        {
            Nome = nome;
            return this;
        }
        public Produto AlterarDescricao(string descricao)
        {
            Descricao = descricao;
            return this;
        }
        public Produto AlterarCategoria(Guid categoriaId)
        {
            CategoriaId = categoriaId;
            return this;
        }
        public Produto AlterarUsuario(Guid usuarioId)
        {
            UsuarioId = usuarioId;
            return this;
        }
        public Produto AlterarPreco(decimal novoPreco)
        {
            Preco = novoPreco;
            return this;
        }
        public Produto AlterarQuantidade(int quantidade)
        {
            Quantidade = quantidade;
            return this;
        }
        #endregion

        public void ValidarEntidade()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .AreNotEquals(CategoriaId, Guid.Empty, "A categoria é inválida.")
                .AreNotEquals(UsuarioId, Guid.Empty, "O usuário é inválido.")
                .IsNotNullOrEmpty(Nome, nameof(Nome), "O nome é obrigatório.")
                .IsNotNullOrEmpty(Descricao, nameof(Descricao), "A descrição é obrigatória.")
                .IsGreaterThan(Preco, 0, "O preço é inválida.")
                .IsGreaterThan(Quantidade, 0, "A quantidade é inválida."));
        }
    }
}
