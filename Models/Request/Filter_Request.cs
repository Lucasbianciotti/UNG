namespace Models.Request
{
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

            Date_Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            Date_End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }
    }
}