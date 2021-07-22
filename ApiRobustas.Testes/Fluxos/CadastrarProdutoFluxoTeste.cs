using ApiRobustas.Compartilhados.ComandosBase;
using ApiRobustas.Dominio.Contextos.Categorias.Repositorios;
using ApiRobustas.Dominio.Contextos.Produtos.Fluxos;
using ApiRobustas.Dominio.Contextos.Produtos.Repositorios;
using ApiRobustas.Dominio.Contextos.Usuarios.Repositorios;
using ApiRobustas.Dominio.UnidadeDeTrabalho;
using ApiRobustas.Testes.Fakes.Comandos;
using AutoFixture;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ApiRobustas.Testes.Fluxos
{
    public class CadastrarProdutoFluxoTeste
    {
        private readonly Mock<IProdutoRepositorio> _produtoRepo;
        private readonly Mock<ICategoriaRepositorio> _categoriaRepo;
        private readonly Mock<IUsuarioRepositorio> _usuarioRepo;
        private readonly Mock<IUnidadeDeTrabalho> _unidadeDeTrabalho;

        private readonly Fixture _fixture;
        private readonly CadastrarProdutoComandoFake _cadastrarProdutoComandoFake;

        public CadastrarProdutoFluxoTeste()
        {
            _produtoRepo = new Mock<IProdutoRepositorio>();
            _categoriaRepo = new Mock<ICategoriaRepositorio>();
            _usuarioRepo = new Mock<IUsuarioRepositorio>();
            _unidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();

            _fixture = new Fixture();
            _cadastrarProdutoComandoFake = new CadastrarProdutoComandoFake(_fixture);
        }

        [Fact]
        [Trait("CadastrarProdutoFluxo", "Cadastro de um novo produto")]
        public async Task DeveGerarNotificacaoQuandoCategoriaForInvalidaNoCadastroDeProduto()
        {
            var comando = _cadastrarProdutoComandoFake.CriarEntidadeValida();

            _categoriaRepo.Setup(x => x.VerificarSeCategoriaExisteAsync(comando.CategoriaId)).ReturnsAsync(false);
            _produtoRepo.Setup(x => x.VerificarSeProdutoExiste(comando.Nome)).Returns(true);
            _usuarioRepo.Setup(x => x.VerificarSeUsuarioExiste(comando.UsuarioId)).Returns(true);
            _unidadeDeTrabalho.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);

            var cadastrarProdutoFluxo = new CadastrarProdutoFluxo(_unidadeDeTrabalho.Object,
                                                                  _produtoRepo.Object,
                                                                  _categoriaRepo.Object,
                                                                  _usuarioRepo.Object);

            var cancelationToken = new CancellationToken();

            var comandoResultado = (ComandoResultado)await cadastrarProdutoFluxo.Handle(comando, cancelationToken);

            Assert.False(comandoResultado.Sucesso);
            Assert.False(cadastrarProdutoFluxo.IsValid);
        }

        [Fact]
        [Trait("CadastrarProdutoFluxo", "Cadastro de um novo produto")]
        public async Task DeveGerarNotificacaoQuandoUsuarioForInvalidoNoCadastroDeProduto()
        {
            var comando = _cadastrarProdutoComandoFake.CriarEntidadeValida();

            _categoriaRepo.Setup(x => x.VerificarSeCategoriaExisteAsync(comando.CategoriaId)).ReturnsAsync(true);
            _produtoRepo.Setup(x => x.VerificarSeProdutoExiste(comando.Nome)).Returns(true);
            _usuarioRepo.Setup(x => x.VerificarSeUsuarioExiste(comando.UsuarioId)).Returns(false);
            _unidadeDeTrabalho.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);

            var cadastrarProdutoFluxo = new CadastrarProdutoFluxo(_unidadeDeTrabalho.Object,
                                                                   _produtoRepo.Object,
                                                                   _categoriaRepo.Object,
                                                                   _usuarioRepo.Object);

            var cancelationToken = new CancellationToken();

            var comandoResultado = (ComandoResultado)await cadastrarProdutoFluxo.Handle(comando, cancelationToken);

            Assert.False(comandoResultado.Sucesso);
            Assert.False(cadastrarProdutoFluxo.IsValid);
        }

        [Fact]
        [Trait("CadastrarProdutoFluxo", "Cadastro de um novo produto")]
        public async Task DeveGerarNotificacaoQuandoProdutoJaExistirNoCadastroDeProduto()
        {
            var comando = _cadastrarProdutoComandoFake.CriarEntidadeValida();

            _categoriaRepo.Setup(x => x.VerificarSeCategoriaExisteAsync(comando.CategoriaId)).ReturnsAsync(true);
            _produtoRepo.Setup(x => x.VerificarSeProdutoExiste(comando.Nome)).Returns(false);
            _usuarioRepo.Setup(x => x.VerificarSeUsuarioExiste(comando.UsuarioId)).Returns(true);
            _unidadeDeTrabalho.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);

            var cadastrarProdutoFluxo = new CadastrarProdutoFluxo(_unidadeDeTrabalho.Object,
                                                                  _produtoRepo.Object,
                                                                  _categoriaRepo.Object,
                                                                  _usuarioRepo.Object);

            var cancelationToken = new CancellationToken();

            var comandoResultado = (ComandoResultado)await cadastrarProdutoFluxo.Handle(comando, cancelationToken);

            Assert.False(comandoResultado.Sucesso);
            Assert.False(cadastrarProdutoFluxo.IsValid);
        }

        [Fact]
        [Trait("CadastrarProdutoFluxo", "Cadastro de um novo produto")]
        public async Task NaoDeveGerarNotificacaoQuandoNoCadastroDeProduto()
        {
            var comando = _cadastrarProdutoComandoFake.CriarEntidadeValida();

            _categoriaRepo.Setup(x => x.VerificarSeCategoriaExisteAsync(comando.CategoriaId)).ReturnsAsync(true);
            _produtoRepo.Setup(x => x.VerificarSeProdutoExiste(comando.Nome)).Returns(true);
            _usuarioRepo.Setup(x => x.VerificarSeUsuarioExiste(comando.UsuarioId)).Returns(true);
            _unidadeDeTrabalho.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);

            var cadastrarProdutoFluxo = new CadastrarProdutoFluxo(_unidadeDeTrabalho.Object,
                                                                  _produtoRepo.Object,
                                                                  _categoriaRepo.Object,
                                                                  _usuarioRepo.Object);

            var cancelationToken = new CancellationToken();

            var comandoResultado = (ComandoResultado)await cadastrarProdutoFluxo.Handle(comando, cancelationToken);

            Assert.True(comandoResultado.Sucesso);
            Assert.True(cadastrarProdutoFluxo.IsValid);
        }
    }
}
