using MeuControle.Dominio.Compartilhado.Enums;
using MeuControle.Dominio.Entidades;
using MeuControle.Dominio.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MeuControle.Api.Controllers
{
    [ApiController]
    [Route("v1/Indicador")]
    public class IndicadorController : ControllerBase
    {
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
        public IList<IndicadorTop5PlanosSaidas> ObterIndicadorTop5PlanosSaidas([FromServices] IIndicadorRepositorio repositorio, Guid usuario, FiltroMes filtroMes)
            => repositorio.ObterIndicadorTop5PlanosSaidas(usuario, filtroMes);
    }
}
