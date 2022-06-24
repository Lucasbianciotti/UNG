using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIClient.LocalModels.SQLite
{
    [Table("Maps")]
    public class Maps
    {
        [Key]
        public long ID { get; set; }

        public string? ContentType { get; set; }

        public string? FileName { get; set; }

        public byte[]? Content { get; set; }
    }
}
