using Models.Enums;
using Models.Request;

namespace Client.Services
{

    public interface IAuthentication_Services
    {
        Task<string> Login(Login_Request userForAuthentication, IToast_Services _Toast);
        Task<string> SignUp(Login_Request userForAuthentication, IToast_Services _Toast);
        Task<string> RestorePassword(Login_RestorePassword_Request Model, IToast_Services _Toast);
        Task<string> UpdatePassword(Login_UpdatePassword_Request Model, IToast_Services _Toast);

        Task Logout();
    }
}
