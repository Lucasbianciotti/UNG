using System.ComponentModel.DataAnnotations;

namespace CommonModels.Request
{
    public class Log_Request
    {
        public Log_Request()
        {
            Filters = new Filter_Request();
        }

        public Filter_Request Filters { get; set; }


        [Key]
        public long ID { get; set; }

        public DateTime Datetime { get; set; }

        public string Type { get; set; }

        public long IDuser { get; set; }
        public string User_Name { get; set; }
        public string User_Email { get; set; }


        public string Detail { get; set; }

    }
}
