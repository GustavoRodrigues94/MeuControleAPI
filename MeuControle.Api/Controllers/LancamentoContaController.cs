using MeuControle.Dominio.Comandos.LancamentoContaComando;
using MeuControle.Dominio.Compartilhado.Contratos;
using MeuControle.Dominio.Compartilhado.Enums;
using MeuControle.Dominio.Entidades;
using MeuControle.Dominio.Manipuladores;
using MeuControle.Dominio.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuControle.Api.Controllers
{
    [ApiController]
    [Route("v1/LancamentoConta")]
    public class LancamentoContaController : ControllerBase
    {
        [Authorize]
        [Route("")]
        [HttpPost]
        public async Task<ActionResult<GenericoComandoResultado>> Criar(
            [FromBody] CriarLancamentoContaComando comando,
            [FromServices] LancamentoContaManipulador manipulador)
            => await manipulador.Manipular(comando);


        [Route("{usuario}/{filtroDias}")]
        [HttpGet]
        public IList<LancamentoConta> ObterLancamentosPorPeriodo([FromServices] ILancamentoContaRepositorio repositorio, Guid usuario, FiltroDias filtroDias) 
            => repositorio.ObterLancamentosPorPeriodo(usuario, filtroDias);
    }
}
