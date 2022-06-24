using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIClient.LocalModels.SQLite
{
    [Table("DataSended")]
    public class DataSended
    {
        [Key]
        public long ID { get; set; }


        [Required]
        public bool Sended { get; set; }


        [Required]
        public string JSONobject { get; set; }


        [Required]
        public string URL { get; set; }

    }
}
