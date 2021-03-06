using Blazored.LocalStorage;
using Models.Enums;
using Models.Request;
using Newtonsoft.Json;
using Web.LocalClass;

namespace Web.Services
{
    public class LocalStorage_Services : ILocalStorage_Services
    {
        private readonly ILocalStorageService _LocalStorage;

        public LocalStorage_Services(ILocalStorageService localStorage)
        {
            _LocalStorage = localStorage;
            //_LocalStorage = localStorage;
        }
        
        
        
        public async Task SetLogin(User_Request user)
        {
            await _LocalStorage.SetItemAsync("user", user);
        }

        public async Task RemoveLogin()
        {
            await _LocalStorage.RemoveItemAsync("user");
        }


        public async Task<User_Request> GetDecodified_Login()
        {
            try
            {
                if (await _LocalStorage.GetItemAsync<User_Request>("user") == null)
                    return null;

                var user = new User_Request();
                user = await _LocalStorage.GetItemAsync<User_Request>("user");

                user.Name = EncryptClass.Decodify(user.Name);
                user.Surname = EncryptClass.Decodify(user.Surname);
                user.CompleteName = EncryptClass.Decodify(user.Name) + " " + EncryptClass.Decodify(user.Surname);
                user.Client = EncryptClass.Decodify(user.Client);
                user.Email = EncryptClass.Decodify(user.Email);

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Permissions_Request> GetPermissionForSection(SystemSectionsEnum section)
        {
            try
            {
                string listpermission = await GetDecodified_JSONPermission();
                var permissions = JsonConvert.DeserializeObject<List<Permissions_Request>>(listpermission);
                if (permissions != null)
                {
                    var permiso = permissions.Where(x => x.IDsection == (int)section).FirstOrDefault();
                    return permiso;
                }
            }
            catch (Exception ex)
            { }

            return new();
        }



        public async Task SetCodified_JSONPermission(string permissions)
        {
            try
            {
                if (await _LocalStorage.GetItemAsync<User_Request>("user") != null)
                {
                    var user = await _LocalStorage.GetItemAsync<User_Request>("user");

                    await _LocalStorage.RemoveItemAsync("user");

                    user.JSONListOfPermissions = permissions;
                    await _LocalStorage.SetItemAsync("user", user);
                }
            }
            catch (Exception)
            {

            }
        }

        public async Task<string> GetDecodified_JSONPermission()
        {
            try
            {
                if (await _LocalStorage.GetItemAsync<User_Request>("user") == null)
                    return null;

                var user = await _LocalStorage.GetItemAsync<User_Request>("user");
                return EncryptClass.Decodify(user.JSONListOfPermissions);
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
