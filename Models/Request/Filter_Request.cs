namespace Models.Request
{
    public class Filter_Request
    {
        public List<long> IDdron { get; set; }

        public List<long> IDusuario { get; set; }

        public DateTime? Fecha_Inicio { get; set; }
        public DateTime? Fecha_Final { get; set; }


        public Filter_Request()
        {
            IDdron = new List<long>();

            IDusuario = new List<long>();

            Fecha_Inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            Fecha_Final = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }
    }
}