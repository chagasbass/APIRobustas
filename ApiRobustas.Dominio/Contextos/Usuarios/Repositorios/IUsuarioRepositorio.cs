using ApiRobustas.Dominio.Contextos.Usuarios.Entidades;
using System;
using System.Threading.Tasks;

namespace ApiRobustas.Dominio.Contextos.Usuarios.Repositorios
{
    public interface IUsuarioRepositorio
    {
        bool VerificarSeUsuarioExiste(string email);
        bool VerificarSeUsuarioExiste(Guid id);
        bool ValidarLogin(string email, string senha);
        Task SalvarUsuarioAsync(Usuario usuario);
        void AtualizarUsuario(Usuario usuario);
        void ExcluirUsuario(Usuario usuario);
    }
}
