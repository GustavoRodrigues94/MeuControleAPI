using System;
using System.Collections.Generic;

namespace MeuControle.Dominio.Entidades
{
    public class Usuario : Entidade
    {
        public Usuario(string nome, string email, string senha, decimal rendaMensal)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            RendaMensal = rendaMensal;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public decimal RendaMensal { get; private set; }
        public virtual ICollection<LancamentoConta> LancamentosContas { get; private set; }
        public virtual ICollection<PlanoConta> PlanosContas { get; private set; }

        public void EsconderSenha() => Senha = "";
    }
}
