using System;
using System.Linq.Expressions;
using MeuControle.Dominio.Entidades;

namespace MeuControle.Dominio.Consultas
{
    public static class PlanoContaConsultas
    {
        public static Expression<Func<PlanoConta, bool>> ObterTodos(Guid usuario) => x => x.CodigoUsuario == usuario;

        public static Expression<Func<PlanoConta, bool>> ObterPorOperacao(Guid usuario, string operacao)
        {
            var charOperacao = operacao.ToLower() == "entrada" ? 'e' : 's';
            return x => x.CodigoUsuario == usuario && x.Operacao == charOperacao;
        }

        public static Expression<Func<PlanoConta, bool>> ObterPorCodigo(Guid codigo, Guid usuario) =>
            x => x.Codigo == codigo && x.CodigoUsuario == usuario;
    }
}
