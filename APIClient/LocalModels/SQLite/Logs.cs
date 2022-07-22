using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIClient.LocalModels.SQLite
{
    [Table("Logs")]
    public partial class Logs
    {
        [Key]
        public long ID { get; set; }

        public long IDuser { get; set; }



        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }


        [Required]
        [StringLength(500)]
        public string Detail { get; set; }

        [Required]
        public string Type { get; set; }

    }

}
