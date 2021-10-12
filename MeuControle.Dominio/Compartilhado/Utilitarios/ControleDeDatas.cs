using MeuControle.Dominio.Compartilhado.Enums;
using System;

namespace MeuControle.Dominio.Compartilhado.Utilitarios
{
    public static class ControleDeDatas
    {
        public static DateTime RetornarDatas(FiltroMes filtroMes, bool dataInicio)
        {
            var dataHoje = DateTime.Today;

            if (filtroMes == FiltroMes.EsteMes)
            {
                return dataInicio
                   ? new DateTime(dataHoje.Year, dataHoje.Month, 1)
                   : new DateTime(dataHoje.Year, dataHoje.Month, DateTime.DaysInMonth(dataHoje.Year, dataHoje.Month), dataHoje.AddHours(23).Hour, dataHoje.AddMinutes(59).Minute, dataHoje.AddSeconds(59).Second);
            }
            else if (filtroMes == FiltroMes.MesPassado)
            {
                var mes = dataHoje.Month;
                var ano = dataHoje.Year;
                if (mes == 1)
                {
                    mes = 12;
                    ano = dataHoje.Year - 1;
                }
                else
                    mes = dataHoje.Month - 1;

                if (dataInicio)
                    return new DateTime(ano, mes, 1);
                else
                    return new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes), dataHoje.AddHours(23).Hour, dataHoje.AddMinutes(59).Minute, dataHoje.AddSeconds(59).Second);
            }
            else
                return new DateTime();
        }

        public static DateTime RetornarDatas(FiltroDias filtroDias, bool dataInicio)
        {
            var dataHoje = DateTime.Today;

            if (filtroDias == FiltroDias.DoisDias)
                return dataInicio
                    ? new DateTime(dataHoje.Year, dataHoje.Month, dataHoje.Day).AddDays(-2)
                    : new DateTime(dataHoje.Year, dataHoje.Month, dataHoje.Day);
            else if (filtroDias == FiltroDias.CincoDias)
                return dataInicio
                    ? new DateTime(dataHoje.Year, dataHoje.Month, dataHoje.Day).AddDays(-5)
                    : new DateTime(dataHoje.Year, dataHoje.Month, dataHoje.Day);
            else if (filtroDias == FiltroDias.TrintaDias)
                return dataInicio
                    ? new DateTime(dataHoje.Year, dataHoje.Month, dataHoje.Day).AddDays(-30)
                    : new DateTime(dataHoje.Year, dataHoje.Month, dataHoje.Day);
            else
                return new DateTime();
        }

        public static int RetornarQuantidadeDiasRestantes(FiltroDiaSemanaMes filtroDiaSemanaMes)
        {
            var dataHoje = DateTime.Today;

            switch (filtroDiaSemanaMes)
            {
                case FiltroDiaSemanaMes.Dia:
                    return DateTime.DaysInMonth(dataHoje.Year, dataHoje.Month) - dataHoje.Day;
                case FiltroDiaSemanaMes.Semana:
                    return 7;
                case FiltroDiaSemanaMes.Mes:
                    return 15;
                default:
                    return 0;
            }
        }
    }
}
