using ApiRobustas.Dominio.Contextos.Usuarios.Entidades;
using ApiRobustas.Testes.Bases;
using ApiRobustas.Testes.Fakes.ObjetosDeValor;
using AutoFixture;

namespace ApiRobustas.Testes.Fakes.Entidades
{
    public class UsuarioFake : IFake<Usuario>
    {
        private readonly Fixture _fixture;
        private readonly NomeFake _nomeFake;

        public UsuarioFake(Fixture fixture)
        {
            _fixture = fixture;
            _nomeFake = new NomeFake(_fixture);
        }

        public Usuario CriarEntidadeInvalida()
        {
            var entidade = _fixture.Build<Usuario>()
                .Create();

            entidade.AlterarNome(_nomeFake.CriarEntidadeInvalida());

            return entidade;
        }

        public Usuario CriarEntidadeValida()
        {
            var entidade = _fixture.Build<Usuario>()
               .Create();

            entidade.AlterarNome(_nomeFake.CriarEntidadeValida());
            entidade.AlterarEmail("teste@teste.com");

            return entidade;
        }
    }
}
