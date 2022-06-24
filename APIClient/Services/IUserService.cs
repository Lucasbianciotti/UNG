using APIClient.LocalModels.SQLite;
using Models.Request;

namespace APIClient.Services
{
    public interface IUserService
    {
        Response_Login_Request Login(Users userAdmin, string IP);
        bool UpdatePassword(Login_UpdatePassword_Request model);
        bool RestorePassword(Login_RestorePassword_Request model);
    }
}
