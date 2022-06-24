using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class SystemMove_Request
    {
        public SystemMove_Request()
        {
            Filters = new Filter_Request();
        }

        public Filter_Request Filters { get; set; }


        [Key]
        public long ID { get; set; }

        public DateTime Date { get; set; }

        public long IDuser { get; set; }
        public string User_Name { get; set; }
        public string User_Email { get; set; }


        public string Detail { get; set; }

    }
}
