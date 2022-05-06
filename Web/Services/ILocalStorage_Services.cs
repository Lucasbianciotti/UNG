using Models.Enums;
using Models.Request;

namespace Web.Services
{
    public interface ILocalStorage_Services
    {
        public Task<User_Request> GetDecodified_Login();
        public Task SetLogin(User_Request user);
        public Task RemoveLogin();
        public Task<Permissions_Request> GetPermissionForSection(SystemSectionsEnum section);
        public Task<string> GetDecodified_JSONPermission();
        public Task SetCodified_JSONPermission(string permission);

    }
}
