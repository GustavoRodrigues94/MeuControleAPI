using MeuControle.Dominio.Compartilhado.Enums;
using MeuControle.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuControle.Dominio.Consultas.IndicadorConsultas.ViewModels;

namespace MeuControle.Dominio.Repositorios
{
    public interface ILancamentoContaRepositorio
    {
        void Criar(LancamentoConta lancamentoConta);

        IList<LancamentoConta> ObterLancamentosPorPeriodo(Guid usuario, FiltroDias filtroDias);

        Task<List<IndicadorTop5PlanosSaidasViewModel>> ObterLancamentosPorPeriodoAgrupado(Guid usuario,
            FiltroMes filtroMes, char entradaOuSaida);
    }
}
