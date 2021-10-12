using MeuControle.Dominio.Compartilhado.Enums;
using MeuControle.Dominio.Entidades;
using MeuControle.Dominio.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuControle.Dominio.Consultas.IndicadorConsultas;
using MeuControle.Dominio.Consultas.IndicadorConsultas.ViewModels;

namespace MeuControle.Api.Controllers
{
    [ApiController]
    [Route("v1/Indicador")]
    public class IndicadorController : ControllerBase
    {
        private readonly IIndicadorConsulta _indicadorConsulta;

        public IndicadorController(IIndicadorConsulta indicadorConsulta)
        {
            _indicadorConsulta = indicadorConsulta;
        }

        [Authorize]
        [Route("Saldo/{usuario}/{filtroMes}")]
        [HttpGet]
        public decimal ObterSaldo([FromServices] IIndicadorRepositorio repositorio, Guid usuario, FiltroMes filtroMes)
            => repositorio.ObterSaldo(usuario, filtroMes);

        [Authorize]
        [Route("SaldoPodeGastar/{usuario}/{filtroDiaSemanaMes}")]
        [HttpGet]
        public decimal ObterSaldoPodeGastar([FromServices] IIndicadorRepositorio repositorio, Guid usuario, FiltroDiaSemanaMes filtroDiaSemanaMes)
            => repositorio.ObterSaldoPodeGastar(usuario, filtroDiaSemanaMes);

        [Authorize]
        [Route("Top5Gastos/{usuario}/{filtroMes}")]
        [HttpGet]
        public async Task<IEnumerable<IndicadorTop5PlanosSaidasViewModel>> ObterIndicadorTop5PlanosSaidas(Guid usuario, FiltroMes filtroMes)
            => await _indicadorConsulta.ObterIndicadorTop5PlanosSaidas(usuario, filtroMes);
    }
}
