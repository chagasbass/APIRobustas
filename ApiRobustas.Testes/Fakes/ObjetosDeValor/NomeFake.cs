using ApiRobustas.Dominio.Contextos.Usuarios.ObjetosDeValor;
using ApiRobustas.Testes.Bases;
using AutoFixture;

namespace ApiRobustas.Testes.Fakes.ObjetosDeValor
{
    public class NomeFake : IFake<Nome>
    {
        private readonly Fixture _fixture;

        public NomeFake(Fixture fixture)
        {
            _fixture = fixture;
        }

        public Nome CriarEntidadeInvalida()
        {
            var entidade = _fixture.Build<Nome>()
                                  .Create();

            return entidade.AlterarPrimeiroNome(string.Empty);
        }

        public Nome CriarEntidadeValida()
        {
            var entidade = _fixture.Build<Nome>()
                                 .Create();

            return entidade;
        }
    }
}
