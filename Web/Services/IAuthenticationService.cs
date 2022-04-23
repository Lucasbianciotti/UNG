using Models.Request;
using System.Threading.Tasks;

namespace Web.Services
{

    public interface IAuthenticationService
    {
        Task<string> GetLogin_CompanyName();

        Task<Usuario_Request> GetLogin();

        Task<string> Login(Login_Request userForAuthentication);
        Task<string> ReestablecerContrase�a(Login_ReestablecerContrase�a_Request Model);
        Task<string> ActualizarContrase�a(Login_ActualizarContrase�a_Request Model);

        Task Logout();
    }
}
