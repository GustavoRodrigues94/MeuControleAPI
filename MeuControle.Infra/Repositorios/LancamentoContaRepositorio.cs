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
    }
}
