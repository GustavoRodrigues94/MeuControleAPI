using Flunt.Notifications;
using Flunt.Validations;
using MeuControle.Dominio.Compartilhado.Contratos;

namespace MeuControle.Dominio.Comandos.UsuarioComando
{
    public class AutenticarUsuarioComando : Notifiable, IComando
    {
        public AutenticarUsuarioComando() {}

        public AutenticarUsuarioComando(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public string Email { get; set; }
        public string Senha { get; set; }

        public void Validate() => AddNotifications(new Contract()
            .IsNotNullOrWhiteSpace(Email, "Email", "Por favor, informe o email")
            .IsNotNullOrWhiteSpace(Senha, "Senha", "Por favor, informe a senha")
        );
    }
}
