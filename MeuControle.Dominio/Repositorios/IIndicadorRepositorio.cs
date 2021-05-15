using MeuControle.Dominio.Compartilhado.Enums;
using MeuControle.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace MeuControle.Dominio.Repositorios
{
    public interface IIndicadorRepositorio
    {
        decimal ObterSaldo(Guid usuario, FiltroMes filtroMes);
        decimal ObterSaldoPodeGastar(Guid usuario, FiltroDiaSemanaMes filtroDiaSemanaMes);
        IList<IndicadorTop5PlanosSaidas> ObterIndicadorTop5PlanosSaidas(Guid usuario, FiltroMes filtroMes);
        
    }
}
