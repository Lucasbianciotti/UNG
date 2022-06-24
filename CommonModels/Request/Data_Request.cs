using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class Data_Request
    {
        public Data_Request()
        {
            Filters = new();
        }

        [Key]
        public long ID { get; set; }

        public bool Sended { get; set; }
        public DateTime? Sended_Datetime { get; set; }

        public Filter_Request? Filters { get; set; }


        public long Equipment_ID { get; set; }
        public int Equipment_Type { get; set; }
        public string Equipment_Name { get; set; }


        [Required(ErrorMessage = "The datetime is required")]
        public DateTime Datetime { get; set; }


        [Required(ErrorMessage = "Timespan is required.")]
        public long Timespan { get; set; }



        [Required(ErrorMessage = "Equipment ID is required.")]
        public long IDequipment { get; set; }



        public string Ubication { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }



        [Required(ErrorMessage = "The count is required.")]
        public int Count { get; set; }


        [Required(ErrorMessage = "The data is required.")]
        public string Info { get; set; }


        [Required(ErrorMessage = "The data is required.")]
        public int Altitude { get; set; }
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


        [Required(ErrorMessage = "The data is required.")]
        public double lat { get; set; }


        [Required(ErrorMessage = "The data is required.")]
        public double lon { get; set; }

        [Required(ErrorMessage = "The data is required.")]
        public int alt { get; set; }
    }

}
