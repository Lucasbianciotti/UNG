using System.ComponentModel.DataAnnotations;

namespace Models.Request
{

    public class Dron_Request
    {
        public Dron_Request()
        {

        }

        [Key]
        public long ID { get; set; }

        public string Aux { get; set; }
    }

    public class EliminarDron_Request
    {
        [Required]
        public long ID { get; set; }
    }
}
