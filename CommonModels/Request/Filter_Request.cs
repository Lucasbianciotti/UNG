namespace CommonModels.Request
{
    public class FilterData_Request
    {
        public List<long> IDequipment { get; set; }
        public bool? Sended { get; set; }

        public DateTime Date_Start { get; set; }
        public DateTime Date_End { get; set; }


        public FilterData_Request()
        {
            IDequipment = new List<long>();
            Sended = null;

            Date_Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            Date_End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }
    }



    public class Filter_Request
    {
        public List<long> IDequipment { get; set; }

        public List<long> IDuser { get; set; }

        public DateTime? Date_Start { get; set; }
        public DateTime? Date_End { get; set; }


        public Filter_Request()
        {
            IDequipment = new List<long>();
            IDuser = new List<long>();

            Date_Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Date_Start -= TimeSpan.FromDays(7);
            Date_End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }
    }
}