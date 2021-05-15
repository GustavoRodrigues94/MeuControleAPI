using System;
using System.Collections.Generic;

namespace MeuControle.Dominio.Entidades
{
    public class PlanoConta : Entidade
    {
        public PlanoConta()
        {
        }

        public PlanoConta(string nome, char operacao, Guid codigoUsuario)
        {
            Nome = nome;
            Operacao = operacao;
            CodigoUsuario = codigoUsuario;
        }

        public Guid CodigoUsuario { get; private set; }
        public Usuario Usuario { get; private set; }
        public string Nome { get; private set; }
        public char Operacao { get; private set; }
        public virtual ICollection<LancamentoConta> LancamentosContas { get; private set; }

        public void Atualizar(string nome, char operacao)
        {
            Nome = nome;
            Operacao = operacao;
        }

    }
}
