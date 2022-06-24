using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIClient.LocalModels.SQLite
{
    [Table("Logs_SystemMoves")]
    public partial class Logs_SystemMoves
    {
        [Key]
        public long ID { get; set; }

        public long IDuser { get; set; }


        [Required]
        [StringLength(50)]
        public string IDstatus { get; set; }


        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }


        [Required]
        [StringLength(500)]
        public string Detail { get; set; }

    }

}
