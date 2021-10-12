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
    }
}
