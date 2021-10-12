using System;
using Flunt.Notifications;
using Flunt.Validations;
using MeuControle.Dominio.Compartilhado.Contratos;

namespace MeuControle.Dominio.Comandos.PlanoContaComando
{
    public class DeletarPlanoContaComando : Notifiable, IComando
    {
        public DeletarPlanoContaComando() { }

        public DeletarPlanoContaComando(Guid planoConta, Guid usuario)
        {
            CodigoPlanoConta = planoConta;
            CodigoUsuario = usuario;
        }

        public Guid CodigoPlanoConta { get; set; }
        public Guid CodigoUsuario { get; set; }

        public void Validate() => AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(CodigoUsuario.ToString(), "Usuario", "Por favor, informe o usuário")
                .IsNotNullOrWhiteSpace(CodigoPlanoConta.ToString(), "Plano conta", "Por favor, informe o plano de conta")
        );
    }
}
