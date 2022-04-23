using System;

namespace API.LocalClass
{
    public class GlobalClass
    {
        internal static DateTime FechaInicialDeFiltro(DateTime? fechaEmision_Inicio)
        {
            try
            {
                return fechaEmision_Inicio.Value.Date;
            }
            catch (Exception)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
        }

        internal static DateTime FechaFinalDeFiltro(DateTime? fechaEmision_Final)
        {
            try
            {
                return new DateTime(fechaEmision_Final.Value.Year, fechaEmision_Final.Value.Month,
                    DateTime.DaysInMonth(fechaEmision_Final.Value.Year, fechaEmision_Final.Value.Month), 23, 59, 59);
            }
            catch (Exception)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59);
            }
        }
    }
}
