using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class Equipment_Request
    {
        public Equipment_Request()
        {
            Filters = new();
            ListOfData = new();
        }

        [Key]
        public long ID { get; set; }

        public Filter_Request? Filters { get; set; }

        public long Modify_IDuser { get; set; }
        public DateTime Modify_Date { get; set; }

        public string IDstatus { get; set; }



        public long? IDstation { get; set; }


        [Required(ErrorMessage = "The name is required.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "The type is required.")]
        [Range(1, 100, ErrorMessage = "The type is required.")]
        public int Type { get; set; }


        public string? QRcode { get; set; }
        public string? QRcodeSRC { get; set; }



        public string? MAC { get; set; }

        public List<Data_Request> ListOfData { get; set; }

    }


    public class DeleteEquipment_Request
    {
        [Required]
        public long ID { get; set; }

        [Required]
        public long IDstation { get; set; }

        public Filter_Request Filters { get; set; }

    }
}
