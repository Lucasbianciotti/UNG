using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class InformationOfData_Request
    {
        public InformationOfData_Request()
        {
            ListOfData = new();
            Filters = new();
        }

        public Filter_Request Filters { get; set; }
        public List<Data_Request> ListOfData { get; set; }

    }

    public class Data_Request
    {
        public Data_Request()
        {
            Filters = new();
            Equipment = new();
        }

        [Key]
        public long ID { get; set; }

        public Filter_Request? Filters { get; set; }


        [Required(ErrorMessage = "The datetime is required")]
        public DateTime Datetime { get; set; }


        [Required(ErrorMessage = "Timespan is required.")]
        public long Timespan { get; set; }


        [Required(ErrorMessage = "Equipment ID is required.")]
        public long IDequipment { get; set; }


        public Equipment_Request Equipment { get; set; }



        [Required(ErrorMessage = "The count is required.")]
        public int Count { get; set; }


        [Required(ErrorMessage = "The data is required.")]
        public string Info { get; set; }



        public string? Aux { get; set; }
    }

    public class Disparo_Request
    {
        [Required(ErrorMessage = "The datetime is required")]
        public long timestamp { get; set; }


        [Required(ErrorMessage = "The count is required.")]
        public int count { get; set; }


        [Required(ErrorMessage = "The ID drone is required.")]
        public long drone { get; set; }


        [Required(ErrorMessage = "The data is required.")]
        public string data { get; set; }
    }

}
