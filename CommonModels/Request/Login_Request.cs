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
        public Response_Login_Request(Client_Request client, Station_Request station, User_Request user, bool isAuth, string token)
        {
            Client = client;
            Station = station;
            User = user;
            IsAuthSuccessful = isAuth;
            Token = token;
        }

        public Client_Request Client { get; set; }
        public Station_Request Station { get; set; }
        public User_Request User { get; set; }

        public bool IsAuthSuccessful { get; set; }
        public string Token { get; set; }

    }

}
