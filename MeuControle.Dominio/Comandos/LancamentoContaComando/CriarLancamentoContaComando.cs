using Flunt.Notifications;
using Flunt.Validations;
using MeuControle.Dominio.Compartilhado.Contratos;
using MeuControle.Dominio.Entidades;
using System;

namespace MeuControle.Dominio.Comandos.LancamentoContaComando
{
    public class CriarLancamentoContaComando : Notifiable, IComando
    {
        public CriarLancamentoContaComando() { }

        public CriarLancamentoContaComando(Guid usuario, char operacao, Guid codigoPlanoConta, DateTime dataLancamento, decimal valor)
        {
            Usuario = usuario;
            Operacao = operacao;
            CodigoPlanoConta = codigoPlanoConta;
            DataLancamento = dataLancamento;
            Valor = valor;
        }

        public Guid Usuario { get; set; }
        public Guid CodigoPlanoConta { get; set; }
        public char Operacao { get; set; }
        public DateTime DataLancamento { get; set; }
        public decimal Valor { get; set; }

        public void Validate() => AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Usuario.ToString(), "Usuário", "Usuário não informado")
                .IsNotNullOrWhiteSpace(CodigoPlanoConta.ToString(), "PlanoConta", "Plano de conta não informado")
                .IsNotNullOrWhiteSpace(Operacao.ToString(), "Operacao", "Operação não informada")
                .IsNotNullOrWhiteSpace(DataLancamento.ToString(), "DataLancamento", "Data de lançamento não informada")
                .IsNotNullOrWhiteSpace(Valor.ToString(), "Valor", "Valor de lançamento não informado")
        );
    }
}
