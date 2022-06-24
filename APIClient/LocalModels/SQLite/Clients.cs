using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIClient.LocalModels.SQLite
{
    [Table("Clients")]
    public partial class Clients
    {
       
        [Key]
        public long ID { get; set; }

        public long IDung { get; set; }

        public long Modify_IDuser { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Modify_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string IDstatus { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }


    }

}
