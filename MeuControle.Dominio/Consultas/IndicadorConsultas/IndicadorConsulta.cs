using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuControle.Dominio.Compartilhado.Enums;
using MeuControle.Dominio.Consultas.IndicadorConsultas.ViewModels;
using MeuControle.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace MeuControle.Dominio.Consultas.IndicadorConsultas
{
    public class IndicadorConsulta : IIndicadorConsulta
    {
        private readonly ILancamentoContaRepositorio _lancamentoContaRepositorio;

        public IndicadorConsulta(ILancamentoContaRepositorio lancamentoContaRepositorio) =>
            _lancamentoContaRepositorio = lancamentoContaRepositorio;

        public async Task<IEnumerable<IndicadorTop5PlanosSaidasViewModel>> ObterIndicadorTop5PlanosSaidas(Guid usuario, FiltroMes filtroMes)
        {
            var listaIndicadorTop5PlanosSaidas = new List<IndicadorTop5PlanosSaidasViewModel>();
            var planosSaidaAgrupados = await _lancamentoContaRepositorio.ObterLancamentosPorPeriodoAgrupado(usuario, filtroMes, 's');

            if (planosSaidaAgrupados?.Count > 5)
            {
                listaIndicadorTop5PlanosSaidas.AddRange(planosSaidaAgrupados.Take(4));
                planosSaidaAgrupados.RemoveRange(0, 4);
                listaIndicadorTop5PlanosSaidas.Add(new IndicadorTop5PlanosSaidasViewModel
                {
                    NomePlanoConta = "Outras saídas", 
                    Valor = planosSaidaAgrupados.Sum(p => p.Valor)
                });
            }
            else
                listaIndicadorTop5PlanosSaidas = planosSaidaAgrupados;

            return listaIndicadorTop5PlanosSaidas;
        }
    }
}
