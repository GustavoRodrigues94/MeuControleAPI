using System;
using Flunt.Notifications;
using Flunt.Validations;
using MeuControle.Dominio.Compartilhado.Contratos;

namespace MeuControle.Dominio.Comandos.PlanoContaComando
{
    public class AtualizarPlanoContaComando : Notifiable, IComando
    {
        public AtualizarPlanoContaComando() { }

        public AtualizarPlanoContaComando(Guid codigo, Guid usuario, string nome, char operacao)
        {
            Codigo = codigo;
            Usuario = usuario;
            Nome = nome;
            Operacao = operacao;
        }

        public Guid Codigo { get; set; }
        public Guid Usuario { get; set; }
        public string Nome { get; set; }
        public char Operacao { get; set; }

        public void Validate() => AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Nome, "Nome", "Por favor, preencha o nome.")
                .HasMaxLen(Nome, 200, "Nome", "Máximo 200 caracteres")
                .IsNotNullOrWhiteSpace(Operacao.ToString(), "Operacao", "Por favor, informe a operação")
                .IsNotNullOrWhiteSpace(Codigo.ToString(), "Usuário", "Usuário não informado")
                .IsNotNullOrWhiteSpace(Usuario.ToString(), "Usuário", "Plano de conta não informado")
        );
    }
}
