using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIClient.LocalModels.SQLite
{
    [Table("Users")]
    public partial class Users
    {

        [Key]
        public long ID { get; set; }

        public long IDung { get; set; }

        [Required]
        public string IDstatus { get; set; }

        public int IDrole { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string DateOf_Creation { get; set; }
        public string DateOf_LastLogin { get; set; }
        public string PinRestorePassword { get; set; }
        public string JSONListOfPermissions { get; set; }

    }
}
