using ApiRobustas.Api.Middlewares;
using ApiRobustas.Compartilhados.Saude;
using ApiRobustas.Dominio.Contextos.Categorias.Fluxos;
using ApiRobustas.Dominio.Contextos.Categorias.Repositorios;
using ApiRobustas.Dominio.Contextos.Produtos.Fluxos;
using ApiRobustas.Dominio.Contextos.Produtos.Repositorios;
using ApiRobustas.Dominio.Contextos.Usuarios.Fluxos;
using ApiRobustas.Dominio.Contextos.Usuarios.Repositorios;
using ApiRobustas.Dominio.Contextos.Usuarios.ServicosDeDominio;
using ApiRobustas.Dominio.Contextos.Usuarios.ServiçosExternos;
using ApiRobustas.Dominio.UnidadeDeTrabalho;
using ApiRobustas.Infraestrutura.Autenticacao.Servicos;
using ApiRobustas.Infraestrutura.Cache.Servicos;
using ApiRobustas.Infraestrutura.Data.Contextos.Categorias.Repositorios;
using ApiRobustas.Infraestrutura.Data.Contextos.Produtos.Repositorios;
using ApiRobustas.Infraestrutura.Data.Contextos.Usuarios.Repositorios;
using ApiRobustas.Infraestrutura.Data.ContextosDeDados;
using ApiRobustas.Infraestrutura.Data.UnidadesDeTrabalho;
using ApiRobustas.Infraestrutura.ServicosExternos.Configuracoes;
using ApiRobustas.Infraestrutura.ServicosExternos.Externos;
using ApiRobustas.Infraestrutura.ServicosExternos.Servicos;
using ApiRobustas.Logs.Servicos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiRobustas.Api.Configuracoes
{
    /// <summary>
    /// Extensao para resolução de dependencias
    /// </summary>
    public static class InjecaoDeDependenciaConfig
    {

        public static void ResolverDependenciasDeLog(this IServiceCollection services)
        {
            //servico de log
            services.AddSingleton<ILogServico, LogServico>();
            services.AddSingleton<InformacaoLog>();
        }
        public static void ResolverDependenciasDaAplicacao(this IServiceCollection services)
        {
            /*Scoped -> 1 vez por para todas as dependencias req
             *Transient ->  toda vez q tiver dependencia é criado novamente
             *Sigleton -> Uma única vez até a aplicação morrer
             */

            //servicos de autenticacao
            services.AddScoped<ITokenServico, TokenServico>();

            //unidade de trabalho
            services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();

            //servico externos
            services.AddScoped<IResilienciaServico, ResilienciaServico>();
            services.AddScoped<IEnderecoServicoExterno, EnderecoServicoExterno>();
            //repo de leitura
            services.AddScoped<IProdutoQueryRepositorio, ProdutoQueryRepositorio>();
            services.AddScoped<ICategoriaQueryRepositorio, CategoriaQueryRepositorio>();


            //servico de dominio
            services.AddScoped<IEnderecoServicoDeDominio, EnderecoServicoDeDominio>();

            //serviço de cache
            services.AddScoped<IProdutoMemoriaCacheServico, ProdutoMemoriaCacheServico>();

            //repos de escrita
            services.AddScoped<IEnderecoRepositorio, EnderecoRepositorio>();
            services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            //fluxos
            services.AddMediatR(typeof(CadastrarCategoriaFluxo).Assembly);
            services.AddMediatR(typeof(CadastrarProdutoFluxo).Assembly);
            services.AddMediatR(typeof(CadastrarUsuarioFluxo).Assembly);
            services.AddMediatR(typeof(EfetuarLoginFluxo).Assembly);
        }

        /// <summary>
        /// Resolve as dependencias dos contextos de dados
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ResolverDependenciasDeContextosDeDados(this IServiceCollection services, IConfiguration configuration)
        {
            var stringDeConexao = configuration.GetConnectionString("BaseDeDados");

            services.AddDbContext<ContextoDeDadosEfCore>(contexto =>
            {
                contexto
                .UseSqlServer(stringDeConexao);

            });

            services.AddScoped<ContextoDeDadosDapper, ContextoDeDadosDapper>();
        }

        public static void AddGlobalExceptionHandlerMiddleware(this IServiceCollection services)
           => services.AddTransient<GlobalExceptionHandlerMiddleware>();
    }
}