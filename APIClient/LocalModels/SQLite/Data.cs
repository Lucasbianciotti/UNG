using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIClient.LocalModels.SQLite
{
    [Table("Data")]
    public partial class Data
    {
        [Key]
        public long ID { get; set; }

        public long IDstation { get; set; }

        public long IDequipment { get; set; }

        public long Timespan { get; set; }


        public bool Sended { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Sended_Datetime { get; set; }


        [Column(TypeName = "datetime")]
        public DateTime Datetime { get; set; }


        public int Count { get; set; }

        [Required]
        public string Info { get; set; }


        public double Lat { get; set; }

        public double Lon { get; set; }

        public int Altitude { get; set; }
    }
}
