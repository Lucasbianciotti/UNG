using Models.Request;

namespace Web.Services
{

    public interface IAuthenticationService
    {
        Task<string> GetLogin_CompanyName();

        Task<User_Request> GetLogin();

        Task<string> Login(Login_Request userForAuthentication);
        Task<string> ReestablecerPassword(Login_RestorePassword_Request Model);
        Task<string> ActualizarPassword(Login_UpdatePassword_Request Model);

        Task Logout();
    }
}
