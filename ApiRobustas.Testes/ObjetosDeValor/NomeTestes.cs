using ApiRobustas.Testes.Fakes.ObjetosDeValor;
using AutoFixture;
using Xunit;

namespace ApiRobustas.Testes.ObjetosDeValor
{
    public class NomeTestes
    {
        private readonly Fixture _fixture;
        private readonly NomeFake _nomeFake;

        public NomeTestes()
        {
            _fixture = new Fixture();
            _nomeFake = new NomeFake(_fixture);
        }

        [Fact]
        [Trait("Nome", "Validação do objeto de valor Nome ")]
        public void DeveRetornarNotificacaoQuandoNomeForInvalido()
        {
            var nome = _nomeFake.CriarEntidadeInvalida();

            Assert.False(nome.IsValid);
        }

        [Fact]
        [Trait("Nome", "Validação do objeto de valor Nome ")]
        public void NaoDeveRetornarNotificacaoQuandoNomeForInvalido()
        {
            var nome = _nomeFake.CriarEntidadeValida();

            Assert.True(nome.IsValid);
        }
    }
}
