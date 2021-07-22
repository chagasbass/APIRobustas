using ApiRobustas.Dominio.Contextos.Produtos.Comandos;
using ApiRobustas.Testes.Bases;
using AutoFixture;
using System;

namespace ApiRobustas.Testes.Fakes.Comandos
{
    public class CadastrarProdutoComandoFake : IFake<CadastrarProdutoComando>
    {
        private readonly Fixture _fixture;

        public CadastrarProdutoComandoFake(Fixture fixture)
        {
            _fixture = fixture;
        }

        public CadastrarProdutoComando CriarEntidadeInvalida()
        {
            var comando = _fixture.Build<CadastrarProdutoComando>()
                                  .Without(x => x.CategoriaId)
                                  .Do(x =>
                                  {
                                      x.CategoriaId = Guid.Empty;
                                  }).Create();

            return comando;
        }

        public CadastrarProdutoComando CriarEntidadeValida()
        {
            var comando = _fixture.Build<CadastrarProdutoComando>()
                                  .Create();

            return comando;
        }
    }
}
