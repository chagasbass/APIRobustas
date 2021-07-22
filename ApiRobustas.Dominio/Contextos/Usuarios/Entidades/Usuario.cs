using ApiRobustas.Compartilhados.EntidadesBase;
using ApiRobustas.Compartilhados.ValidacoesDeDominio;
using ApiRobustas.Dominio.Contextos.Usuarios.ObjetosDeValor;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace ApiRobustas.Dominio.Contextos.Usuarios.Entidades
{
    public class Usuario : Entidade, IValidacaoEntidade
    {
        public Nome Nome { get; private set; }
        public Guid EnderecoId { get; private set; }
        public Endereco Endereco { get; private set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        protected Usuario() { }

        public Usuario(Nome nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public void AlterarNome(Nome nome) => Nome = nome;
        public void AlterarEmail(string email) => Email = email;
        public void AlterarSenha(string senha) => Senha = senha;
        public void AlterarEndereco(Guid enderecoId)
        {
            if (enderecoId.Equals(Guid.Empty))
            {
                AddNotification("EnderecoId", "Endereco inválido.");
                return;
            }

            EnderecoId = enderecoId;
        }
        public void AlterarEndereco(Endereco endereco)
        {
            if (endereco is null)
            {
                AddNotification("Endereco", "Endereco inválido.");
                return;
            }

            Endereco = endereco;
            EnderecoId = endereco.Id;
        }
        public void ValidarEntidade()
        {
            AddNotifications(Nome);

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsEmailOrEmpty(Email, nameof(Email), "o e-mail é inválido.")
                .IsNotNullOrEmpty(Senha, nameof(Senha), "A senha é obrigatória."));

            Senha = ValidacoesCustomizadas.EncriptarSenha(Senha);
        }

        public override string ToString() => $"{Nome.PrimeiroNome} {Nome.Sobrenome}";
    }
}
