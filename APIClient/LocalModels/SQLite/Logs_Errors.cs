using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIClient.LocalModels.SQLite
{
    [Table("Logs_Errors")]
    public partial class Logs_Errors
    {
        [Key]
        public long ID { get; set; }

        public long IDuser { get; set; }


        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }


        [StringLength(150)]
        public string System { get; set; }


        [StringLength(50)]
        public string Code { get; set; }


        [StringLength(250)]
        public string Commentary { get; set; }


        [StringLength(50)]
        public string Action { get; set; }


        [StringLength(50)]
        public string Exception { get; set; }
        public string Exception_File { get; set; }
        public string Exception_Method { get; set; }
        public string Exception_NumberOfLine { get; set; }
        public string Exception_Source { get; set; }
        public string Exception_Message { get; set; }

    }

}
