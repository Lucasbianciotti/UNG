using Models.EntityFrameworks;
using Models.Request;

namespace API.Services
{
    public interface IUserService
    {
        Response_Login_Request Login(Users userAdmin);
        bool UpdatePassword(Login_UpdatePassword_Request model);
        bool RestorePassword(Login_RestorePassword_Request model);
    }
}
