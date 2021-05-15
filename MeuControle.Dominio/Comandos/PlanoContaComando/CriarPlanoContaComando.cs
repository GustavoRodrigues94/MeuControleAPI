using System;
using Flunt.Notifications;
using Flunt.Validations;
using MeuControle.Dominio.Compartilhado.Contratos;

namespace MeuControle.Dominio.Comandos.PlanoContaComando
{
    public class CriarPlanoContaComando : Notifiable, IComando
    {
        public CriarPlanoContaComando() { }

        public CriarPlanoContaComando(string nome, char operacao, Guid usuario)
        {
            Nome = nome;
            Operacao = operacao;
            Usuario = usuario;
        }

        public string Nome { get; set; }
        public char Operacao { get; set; }
        public Guid Usuario { get; set; }

        public void Validate() => AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Nome, "Nome", "Por favor, preencha o nome")
                .HasMaxLen(Nome, 200, "Nome", "Máximo 200 caracteres")
                .IsNotNullOrWhiteSpace(Operacao.ToString(), "Operacao", "Por favor, informe a operação")
                .IsNotNullOrWhiteSpace(Usuario.ToString(), "Usuário", "Usuário não informado")
        );
    }
}
