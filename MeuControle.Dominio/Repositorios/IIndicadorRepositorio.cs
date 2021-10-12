using MeuControle.Dominio.Compartilhado.Enums;
using System;

namespace MeuControle.Dominio.Repositorios
{
    public interface IIndicadorRepositorio
    {
        decimal ObterSaldo(Guid usuario, FiltroMes filtroMes);
        decimal ObterSaldoPodeGastar(Guid usuario, FiltroDiaSemanaMes filtroDiaSemanaMes);
    }
}
