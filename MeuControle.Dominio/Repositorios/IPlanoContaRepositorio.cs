using System;
using System.Collections.Generic;
using MeuControle.Dominio.Entidades;

namespace MeuControle.Dominio.Repositorios
{
    public interface IPlanoContaRepositorio
    {
        void Criar(PlanoConta produto);
        void Atualizar(PlanoConta produto);
        void Deletar(PlanoConta produto);
        PlanoConta ObterPorCodigo(Guid codigo, Guid usuario);
        IEnumerable<PlanoConta> ObterTodos(Guid usuario);
        IEnumerable<PlanoConta> ObterPorOperacao(Guid usuario, string operacao);
    }
}
