namespace CommonModels.Request
{
    public static class LapsosParaFiltro_Dashboard
    {
        public static string Day { get { return "Day"; } }
        public static string Week { get { return "Week"; } }
        public static string Month { get { return "Month"; } }
        public static string Year { get { return "Year"; } }
    }

    public class FilterDashboard_Request
    {
        public DateTime? Date_Start { get; set; }

        public DateTime? Date_End { get; set; }

        public string Lapso { get; set; }

        public FilterDashboard_Request()
        {
            Lapso = LapsosParaFiltro_Dashboard.Month;

            Date_Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            Date_End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }
    }



    public class Dashboard_Request
    {
        public Dashboard_Request()
        {


        }



    }
}
