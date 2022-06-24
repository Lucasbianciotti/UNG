using System.Security.Claims;

namespace Class
{
    public class GlobalClass
    {
        public static DateTime FechaInicialDeFiltro(DateTime? fechaEmision_Inicio)
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

        public static DateTime FechaFinalDeFiltro(DateTime? fechaEmision_Final)
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




        public static long GetID_Client(ClaimsPrincipal _user)
        {
            try
            {
                return long.Parse(_user.FindFirst(ClaimTypes.GroupSid)?.Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static long GetID_User(ClaimsPrincipal _user)
        {
            try
            {
                return long.Parse(_user.FindFirst(ClaimTypes.PrimarySid)?.Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }


    }
}
