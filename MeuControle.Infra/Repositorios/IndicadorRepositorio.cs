using MeuControle.Dominio.Compartilhado.Enums;
using MeuControle.Dominio.Compartilhado.Utilitarios;
using MeuControle.Dominio.Entidades;
using MeuControle.Dominio.Repositorios;
using MeuControle.Infra.Contextos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeuControle.Infra.Repositorios
{
    public class IndicadorRepositorio : IIndicadorRepositorio
    {
        private readonly ContextoDados _contexto;

        public IndicadorRepositorio(ContextoDados contexto) => _contexto = contexto;

        public decimal ObterSaldo(Guid usuario, FiltroMes filtroMes)
        {
            var lancamentosContas = _contexto.LancamentosContas
                                              .Include(p => p.PlanoConta)
                                              .AsNoTracking()
                                              .Where(l => l.CodigoUsuario == usuario
                                                    && l.DataLancamento >= ControleDeDatas.RetornarDatas(filtroMes, true)
                                                    && l.DataLancamento <= ControleDeDatas.RetornarDatas(filtroMes, false));

            var lancamentosEntrada = lancamentosContas.Where(l => l.Operacao == 'e').Sum(l => l.Valor);
            var lancamentosSaida = lancamentosContas.Where(l => l.Operacao == 's').Sum(l => l.Valor);

            return lancamentosEntrada - lancamentosSaida;
        }

        public decimal ObterSaldoPodeGastar(Guid usuario, FiltroDiaSemanaMes filtroDiaSemanaMes)
        {
            var lancamentosContas = _contexto.LancamentosContas
                                              .Include(p => p.PlanoConta)
                                              .AsNoTracking()
                                              .Where(l => l.CodigoUsuario == usuario
                                                   && l.DataLancamento >= ControleDeDatas.RetornarDatas(FiltroMes.EsteMes, true)
                                                   && l.DataLancamento <= ControleDeDatas.RetornarDatas(FiltroMes.EsteMes, false));

            var lancamentosEntrada = lancamentosContas.Where(l => l.Operacao == 'e').Sum(l => l.Valor);
            var lancamentosSaida = lancamentosContas.Where(l => l.Operacao == 's').Sum(l => l.Valor);

            var diasRestantes = ControleDeDatas.RetornarQuantidadeDiasRestantes(filtroDiaSemanaMes);

            switch (filtroDiaSemanaMes)
            {
                case FiltroDiaSemanaMes.Mes:
                    return lancamentosEntrada - lancamentosSaida - 3000;
                case FiltroDiaSemanaMes.Dia:
                    return (lancamentosEntrada - lancamentosSaida - 3000) / diasRestantes;
                case FiltroDiaSemanaMes.Semana:
                    return (lancamentosEntrada - lancamentosSaida - 3000) / 13 * 3;
                default:
                    return 0;
            }
        }

        public IList<IndicadorTop5PlanosSaidas> ObterIndicadorTop5PlanosSaidas(Guid usuario, FiltroMes filtroMes)
        {
            var listaIndicadorTop5PlanosSaidas = new List<IndicadorTop5PlanosSaidas>();
            var planosSaidaAgrupados = _contexto.LancamentosContas
                                              .Include(p => p.PlanoConta)
                                              .AsNoTracking()
                                              .Where(l => l.CodigoUsuario == usuario 
                                                    && l.DataLancamento >= ControleDeDatas.RetornarDatas(filtroMes, true) 
                                                    && l.DataLancamento <= ControleDeDatas.RetornarDatas(filtroMes, false)).ToList()
                                              .Where(p => p.Operacao == 's')
                                              .Select(c => new { c.Valor, c.CodigoPlanoConta, c.PlanoConta.Nome })
                                              .GroupBy(c => c.CodigoPlanoConta, (k, g) => new IndicadorTop5PlanosSaidas
                                              {
                                                  NomePlanoConta = g.Select(b => b.Nome).FirstOrDefault(),
                                                  Valor = g.Sum(b => b.Valor)
                                              })
                                              .OrderByDescending(g => g.Valor).ToList();

            if (planosSaidaAgrupados?.Count > 5)
            {
                listaIndicadorTop5PlanosSaidas.AddRange(planosSaidaAgrupados.Take(4));
                planosSaidaAgrupados.RemoveRange(0, 4);
                listaIndicadorTop5PlanosSaidas.Add(new IndicadorTop5PlanosSaidas { NomePlanoConta = "Outras saídas", Valor = planosSaidaAgrupados.Sum(p => p.Valor) });
            }
            else
                listaIndicadorTop5PlanosSaidas = planosSaidaAgrupados;

            return listaIndicadorTop5PlanosSaidas;
        }
    }
}
