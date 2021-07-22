using ApiRobustas.Testes.Fakes.Comandos;
using AutoFixture;
using Xunit;

namespace ApiRobustas.Testes.Comandos
{
    public class CadastrarUsuarioComandoTestes
    {
        private readonly Fixture _fixture;
        private readonly CadastrarUsuarioComandoFake _cadastrarUsuarioComandoFake;

        public CadastrarUsuarioComandoTestes()
        {
            _fixture = new Fixture();
            _cadastrarUsuarioComandoFake = new CadastrarUsuarioComandoFake(_fixture);
        }

        [Fact]
        [Trait("CadastrarUsuarioComando", "Comando para cadastro de um novo usuário.")]
        public void DeveRetornarNotificacaoQuandoCadastrarUsuarioComandoForInvalido()
        {
            var comando = _cadastrarUsuarioComandoFake.CriarEntidadeInvalida();
            comando.ValidarComando();

            Assert.False(comando.IsValid);
        }

        [Fact]
        [Trait("CadastrarUsuarioComando", "Comando para cadastro de um novo usuário.")]
        public void NaoDeveRetornarNotificacaoQuandoCadastrarUsuarioComandoForValido()
        {
            var comando = _cadastrarUsuarioComandoFake.CriarEntidadeValida();
            comando.ValidarComando();

            Assert.True(comando.IsValid);
        }
    }
}
