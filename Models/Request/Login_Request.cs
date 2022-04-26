using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class Login_Request
    {
        [Required(ErrorMessage = "The email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }


    public class Login_RestorePassword_Request
    {
        [Required(ErrorMessage = "The email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class Login_UpdatePassword_Request
    {
        [Required(ErrorMessage = "The email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is required.")]
        [StringLength(255, ErrorMessage = "La password debe tener entre 6 y 255 carácteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [StringLength(255, ErrorMessage = "La password debe tener entre 6 y 255 carácteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }



    public class Response_Login_Request
    {

        [Key]
        public long ID { get; set; }
        public string Token { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string URL_ImagenDePerfil { get; set; }

        public long IDcompany { get; set; }
        public string Company { get; set; }


        public List<PermisosDeUsuario_Request> PermisosDeUsuario { get; set; }


        public bool IsAuthSuccessful { get; set; }
    }

}
