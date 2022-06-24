using APIAdmin.LocalModels.EntityFrameworks;
using Models.Request;

namespace APIAdmin.Services
{
    public interface IUserService
    {
        Response_Login_Request Login(Users userAdmin);
        bool UpdatePassword(Login_UpdatePassword_Request model);
        bool RestorePassword(Login_RestorePassword_Request model);
    }
}
