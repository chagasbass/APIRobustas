using ApiRobustas.Compartilhados.Configuracoes;
using ApiRobustas.Dominio.Contextos.Usuarios.Comandos;
using ApiRobustas.Dominio.Contextos.Usuarios.ServiçosExternos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiRobustas.Infraestrutura.Autenticacao.Servicos
{
    /// <summary>
    /// Implementação do serviço de token
    /// </summary>
    public class TokenServico : ITokenServico
    {
        public string GerarToken(EfetuarLoginComando efetuarLoginComando)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(ConfiguracoesCompartilhadas.SEGREDO_TOKEN);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, efetuarLoginComando.Email.ToString()),
                    new Claim(ClaimTypes.Role, ConfiguracoesCompartilhadas.USER_DEFAULT_ROLE)
                }),
                Expires = DateTime.UtcNow.AddMinutes(ConfiguracoesCompartilhadas.TEMPO_EXPIRACAO_EM_MINUTOS),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
