using ApiRobustas.Testes.Fakes.Entidades;
using AutoFixture;
using Xunit;

namespace ApiRobustas.Testes.Entidades
{
    public class UsuarioTeste
    {
        private readonly Fixture _fixture;
        private readonly UsuarioFake _usuarioFake;

        public UsuarioTeste()
        {
            _fixture = new Fixture();
            _usuarioFake = new UsuarioFake(_fixture);
        }

        [Fact]
        [Trait("Entidade Usuário", "Validação de entidade ")]
        public void DeveRetornarNotificacaoQuandoUsuarioForInvalido()
        {
            var usuario = _usuarioFake.CriarEntidadeInvalida();
            usuario.ValidarEntidade();

            Assert.False(usuario.IsValid);
            Assert.True(usuario.Notifications.Count > 0);
        }

        [Fact]
        [Trait("Entidade Usuário", "Validação de entidade ")]
        public void NaoDeveRetornarNotificacaoQuandoUsuarioForValido()
        {
            var usuario = _usuarioFake.CriarEntidadeValida();
            usuario.ValidarEntidade();

            Assert.True(usuario.IsValid);
            Assert.False(usuario.Notifications.Count > 0);
        }
    }
}
