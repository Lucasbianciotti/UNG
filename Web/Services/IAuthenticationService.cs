using Models.Request;
using System.Threading.Tasks;

namespace Web.Services
{

    public interface IAuthenticationService
    {
        Task<string> GetLogin_CompanyName();

        Task<Usuario_Request> GetLogin();

        Task<string> Login(Login_Request userForAuthentication);
        Task<string> ReestablecerContraseña(Login_ReestablecerContraseña_Request Model);
        Task<string> ActualizarContraseña(Login_ActualizarContraseña_Request Model);

        Task Logout();
    }
}
