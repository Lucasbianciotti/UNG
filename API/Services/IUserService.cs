using Models.EntityFrameworks;
using Models.Request;

namespace API.Services
{
    public interface IUserService
    {
        Response_Login_Request Login(Usuarios usuarioAdmin);
        bool ActualizarContrase�a(Login_ActualizarContrase�a_Request model);
        bool ReestablecerContrase�a(Login_ReestablecerContrase�a_Request model);
    }
}
