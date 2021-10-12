using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuControle.Dominio.Compartilhado.Enums;
using MeuControle.Dominio.Consultas.IndicadorConsultas.ViewModels;

namespace MeuControle.Dominio.Consultas.IndicadorConsultas
{
    public interface IIndicadorConsulta
    {
        Task<IEnumerable<IndicadorTop5PlanosSaidasViewModel>> ObterIndicadorTop5PlanosSaidas(Guid usuario, FiltroMes filtroMes);
    }
}
