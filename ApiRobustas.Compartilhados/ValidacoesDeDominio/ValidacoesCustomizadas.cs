using ApiRobustas.Compartilhados.Configuracoes;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ApiRobustas.Compartilhados.ValidacoesDeDominio
{
    public static class ValidacoesCustomizadas
    {
        private static readonly List<string> _CepsInvalidos = new()
        {
            "00000000",
            "11111111",
            "22222222",
            "33333333",
            "44444444",
            "55555555",
            "66666666",
            "77777777",
            "99999999",
            "00000-000",
            "11111-111",
            "22222-222",
            "33333-333",
            "44444-444",
            "55555-555",
            "66666-666",
            "77777-777",
            "99999-999",
        };

        public static bool ValidarCep(string cep)
        {
            if (string.IsNullOrEmpty(cep))
                return false;

            if (_CepsInvalidos.Contains(cep))
                return false;

            if (cep.Contains("-"))
            {
                if (cep.Length != 9)
                    return false;

                return Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
            }
            else
            {
                if (cep.Length != 8)
                    return false;
            }

            cep = cep.Insert(5, "-");

            return Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
        }

        public static string EncriptarSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha)) return "";

            senha += ConfiguracoesCompartilhadas.SEGREDO_SENHA;
            var novaSenha = senha;
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(novaSenha));
            var senhaEncriptada = new StringBuilder();

            foreach (var t in data)
                senhaEncriptada.Append(t.ToString("x2"));

            return senhaEncriptada.ToString();
        }
    }
}
