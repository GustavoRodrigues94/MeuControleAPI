using System;

namespace MeuControle.Dominio.Entidades
{
    public class LancamentoConta : Entidade
    {
        public LancamentoConta()
        {
        }

        public LancamentoConta(Guid codigoUsuario, char operacao, Guid codigoPlanoConta, DateTime dataLancamento, decimal valor)
        {
            CodigoUsuario = codigoUsuario;
            CodigoPlanoConta = codigoPlanoConta;
            Operacao = operacao;
            DataLancamento = dataLancamento;
            Valor = valor;
        }

        public Guid CodigoUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public Guid CodigoPlanoConta { get; set; }
        public PlanoConta PlanoConta { get; set; }
        public char Operacao { get; set; }
        public DateTime DataLancamento { get; set; }
        public decimal Valor { get; set; }
    }
}
