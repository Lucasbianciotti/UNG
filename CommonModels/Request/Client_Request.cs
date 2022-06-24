using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class Client_Request
    {
        [Key]
        public long ID { get; set; }

        public long IDung { get; set; }

        public long Modify_IDuser { get; set; }

        public DateTime Modify_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string IDstatus { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
