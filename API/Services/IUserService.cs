using Models.EntityFrameworks;
using Models.Request;

namespace API.Services
{
    public interface IUserService
    {
        Response_Login_Request Login(Usuarios usuarioAdmin);
        bool ActualizarContraseña(Login_ActualizarContraseña_Request model);
        bool ReestablecerContraseña(Login_ReestablecerContraseña_Request model);
    }
}
