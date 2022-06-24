using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIClient.LocalModels.SQLite
{
    [Table("Equipments")]
    public partial class Equipments
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

        public long? IDstation { get; set; }


        [Required]
        [StringLength(50)]
        public string Name { get; set; }


        public int Type { get; set; }


        [StringLength(50)]
        public string MAC { get; set; }

    }

}
