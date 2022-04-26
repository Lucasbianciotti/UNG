using Models.Request;
using System.Threading.Tasks;

namespace Web.Services
{

    public interface IAuthenticationService
    {
        Task<string> GetLogin_CompanyName();

        Task<User_Request> GetLogin();

        Task<string> Login(Login_Request userForAuthentication);
        Task<string> ReestablecerContrase�a(Login_RestorePassword_Request Model);
        Task<string> ActualizarContrase�a(Login_UpdatePassword_Request Model);

        Task Logout();
    }
}
