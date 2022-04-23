namespace Models.Request
{
    public static class LapsosParaFiltro_Dashboard
    {
        public static string Dia { get { return "Dia"; } }
        public static string Semana { get { return "Semana"; } }
        public static string Mes { get { return "Mes"; } }
        public static string Año { get { return "Ano"; } }
    }

    public class FilterDashboard_Request
    {
        public DateTime? Fecha_Inicio { get; set; }

        public DateTime? Fecha_Final { get; set; }

        public string Lapso { get; set; }

        public FilterDashboard_Request()
        {
            Lapso = LapsosParaFiltro_Dashboard.Mes;
            Fecha_Inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            Fecha_Final = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }
    }



    public class Dashboard_Request
    {
        public Dashboard_Request()
        {


        }



    }
}
