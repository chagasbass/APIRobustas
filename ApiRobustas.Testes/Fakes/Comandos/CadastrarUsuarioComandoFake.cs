using ApiRobustas.Dominio.Contextos.Usuarios.Comandos;
using ApiRobustas.Testes.Bases;
using AutoFixture;

namespace ApiRobustas.Testes.Fakes.Comandos
{
    public class CadastrarUsuarioComandoFake : IFake<CadastrarUsuarioComando>
    {
        private readonly Fixture _fixture;

        public CadastrarUsuarioComandoFake(Fixture fixture)
        {
            _fixture = fixture;
        }

        public CadastrarUsuarioComando CriarEntidadeInvalida()
        {
            var comando = _fixture.Build<CadastrarUsuarioComando>()
                                        .Without(x => x.Cep)
                                        .Create();
            return comando;
        }

        public CadastrarUsuarioComando CriarEntidadeValida()
        {
            var comando = _fixture.Build<CadastrarUsuarioComando>()
                                  .Without(x => x.Cep)
                                  .Without(x => x.Email)
                                  .Do(x =>
                                  {
                                      x.Cep = "24130-110";
                                      x.Email = "teste@teste.com";
                                  }).Create();

            return comando;
        }
    }
}
