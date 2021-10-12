using MeuControle.Dominio.Compartilhado.Enums;
using MeuControle.Dominio.Compartilhado.Utilitarios;
using MeuControle.Dominio.Entidades;
using MeuControle.Dominio.Repositorios;
using MeuControle.Infra.Contextos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuControle.Dominio.Consultas.IndicadorConsultas.ViewModels;

namespace MeuControle.Infra.Repositorios
{
    public class LancamentoContaRepositorio : ILancamentoContaRepositorio
    {

        private readonly ContextoDados _contexto;

        public LancamentoContaRepositorio(ContextoDados contexto) => _contexto = contexto;

        public void Criar(LancamentoConta lancamentoConta)
        {
            _contexto.LancamentosContas.Add(lancamentoConta);
            _contexto.SaveChanges();
        }

        public IList<LancamentoConta> ObterLancamentosPorPeriodo(Guid usuario, FiltroDias filtroDias) => _contexto.LancamentosContas
                                                                                                            .Include(p => p.PlanoConta)
                                                                                                            .AsNoTracking()
                                                                                                            .Where(l => l.CodigoUsuario == usuario &&
                                                                                                                        l.DataLancamento >= ControleDeDatas.RetornarDatas(filtroDias, true) &&
                                                                                                                        l.DataLancamento <= ControleDeDatas.RetornarDatas(filtroDias, false))
                                                                                                            .Select(l => new LancamentoConta { PlanoConta = l.PlanoConta, DataLancamento = l.DataLancamento, Valor = l.Valor })
                                                                                                            .OrderBy(l => l.DataLancamento)
                                                                                                            .ToList();

        public async Task<List<IndicadorTop5PlanosSaidasViewModel>> ObterLancamentosPorPeriodoAgrupado(Guid usuario,
            FiltroMes filtroMes, char entradaOuSaida)
        {
            var lancamentosAgrupados = await _contexto.LancamentosContas
                .Include(p => p.PlanoConta)
                .Where(p => p.Operacao == entradaOuSaida)
                .AsNoTrackingWithIdentityResolution()
                .Where(l => l.CodigoUsuario == usuario
                            && l.DataLancamento >= ControleDeDatas.RetornarDatas(filtroMes, true)
                            && l.DataLancamento <= ControleDeDatas.RetornarDatas(filtroMes, false)).ToListAsync();

            return lancamentosAgrupados.Select(c => new {c.Valor, c.CodigoPlanoConta, c.PlanoConta.Nome})
                .GroupBy(c => c.CodigoPlanoConta, (k, g) => new IndicadorTop5PlanosSaidasViewModel
                {
                    NomePlanoConta = g.Select(b => b.Nome).FirstOrDefault(),
                    Valor = g.Sum(b => b.Valor)
                })
                .OrderByDescending(g => g.Valor)
                .ToList();
        }
            
    }
}
