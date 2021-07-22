using ApiRobustas.Dominio.Contextos.Usuarios.Comandos;

namespace ApiRobustas.Dominio.Contextos.Usuarios.ServiçosExternos
{
    public interface ITokenServico
    {
        string GerarToken(EfetuarLoginComando efetuarLoginComando);
    }
}
