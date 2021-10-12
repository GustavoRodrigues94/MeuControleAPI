using Flunt.Notifications;
using Flunt.Validations;
using MeuControle.Dominio.Compartilhado.Contratos;

namespace MeuControle.Dominio.Comandos.UsuarioComando
{
    public class CriarUsuarioComando : Notifiable, IComando
    {
        public CriarUsuarioComando() { }

        public CriarUsuarioComando(string nome, string email, string senha, decimal rendaMensal)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            RendaMensal = rendaMensal;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public decimal RendaMensal { get; set; }

        public void Validate() => AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace(Nome, "Nome", "Por favor, preencha o nome")
            .HasMaxLen(Nome, 200, "Nome", "Máximo 200 caracteres")
            .IsNotNullOrWhiteSpace(Email, "Email", "Por favor, informe o email")
            .IsNotNullOrWhiteSpace(Senha, "Senha", "Por favor, informe a senha")
            .IsBetween(RendaMensal, 0, 100000, "RendaMensal", "Mínimo R$0,00 e máximo R$100.000,00")
        );
    }
}
