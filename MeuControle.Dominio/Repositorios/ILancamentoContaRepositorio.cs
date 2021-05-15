using MeuControle.Dominio.Compartilhado.Enums;
using MeuControle.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace MeuControle.Dominio.Repositorios
{
    public interface ILancamentoContaRepositorio
    {
        void Criar(LancamentoConta lancamentoConta);
        IList<LancamentoConta> ObterLancamentosPorPeriodo(Guid usuario, FiltroDias filtroDias);
    }
}
