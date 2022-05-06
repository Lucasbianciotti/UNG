using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class Permissions_Request
    {
        public Permissions_Request()
        {
            Read = false;
            Create = false;
            Modify = false;
            Delete = false;
            Export = false;
        }

        [Key]
        public int IDsection { get; set; }

        public bool Read { get; set; }
        public bool Create { get; set; }
        public bool Modify { get; set; }
        public bool Delete { get; set; }
        public bool Export { get; set; }
    }

    public class User_Request
    {
        public User_Request()
        {

        }

        [Key]
        public long ID { get; set; }


        public string IDstatus { get; set; }


        [Required(ErrorMessage = "The rol is required.")]
        public int IDrole { get; set; }


        public string CompleteName { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The surname is required.")]
        public string Surname { get; set; }



        [Required(ErrorMessage = "The email is required.")]
        [EmailAddress(ErrorMessage = "The email is not valid.")]
        public string Email { get; set; }
        public string Password { get; set; }


        public long? IDclient { get; set; }
        public string Client { get; set; }


        //public Permissions_Request PermissionsForSection { get; set; }
        public string JSONListOfPermissions { get; set; }

        public string Aux { get; set; }
    }

    public class DeleteUser_Request
    {
        public DeleteUser_Request()
        {
            Filters = new();
        }

        [Required]
        public long ID { get; set; }
        public Filter_Request Filters { get; set; }

    }

    public class ChangePasswordUser_Request
    {

        [Required(ErrorMessage = "Ingrese la password anterior")]
        [DataType(DataType.Password)]
        public string PasswordOld { get; set; }


        [Required(ErrorMessage = "Ingrese la nueva password")]
        [StringLength(255, ErrorMessage = "La password debe tener al menos 5 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Confirme la nueva password")]
        [StringLength(255, ErrorMessage = "La password debe tener al menos 5 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("NuevaPassword", ErrorMessage = "La nueva password no coincide")]
        public string RepeatNewPassword { get; set; }

    }
}
