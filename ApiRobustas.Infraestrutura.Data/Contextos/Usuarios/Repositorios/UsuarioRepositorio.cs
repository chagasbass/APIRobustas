using ApiRobustas.Dominio.Contextos.Usuarios.Entidades;
using ApiRobustas.Dominio.Contextos.Usuarios.Repositorios;
using ApiRobustas.Infraestrutura.Data.ContextosDeDados;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRobustas.Infraestrutura.Data.Contextos.Usuarios.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ContextoDeDadosEfCore _Contexto;

        public UsuarioRepositorio(ContextoDeDadosEfCore contexto)
        {
            _Contexto = contexto;
        }

        public void AtualizarUsuario(Usuario usuario) => _Contexto.Usuarios.Update(usuario);

        public void ExcluirUsuario(Usuario usuario) => _Contexto.Usuarios.Remove(usuario);

        public async Task SalvarUsuarioAsync(Usuario usuario) => await _Contexto.Usuarios.AddAsync(usuario);

        public bool ValidarLogin(string email, string senha)
        {
            var usuarioEncontrado = _Contexto.Usuarios.FirstOrDefault(x => x.Email == email && x.Senha == senha);

            return usuarioEncontrado is not null;
        }

        public bool VerificarSeUsuarioExiste(string email)
        {
            var usuarioEncontrado = _Contexto.Usuarios.FirstOrDefault(x => x.Email.ToLower().Equals(email.ToLower()));
            return usuarioEncontrado is not null;
        }

        public bool VerificarSeUsuarioExiste(Guid id)
        {
            var usuarioEncontrado = _Contexto.Usuarios.FirstOrDefault(x => x.Id == id);
            return usuarioEncontrado is not null;
        }
    }
}
