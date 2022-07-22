using CommonModels.Enums;
using CommonModels.Request;

namespace Client.Services
{
    public interface ILocalStorage_Services
    {
        public Task<User_Request> GetDecodified_Login();
        public Task SetLogin(Response_Login_Request login);
        public Task RemoveLogin();
        public Task<Permissions_Request> GetPermissionForSection(SystemSectionsEnum section);
        public Task<string> GetDecodified_JSONPermission();
        public Task SetCodified_JSONPermission(string permission);

    }
}
