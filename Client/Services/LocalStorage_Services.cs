using Blazored.LocalStorage;
using Models.Enums;
using Models.Request;
using Newtonsoft.Json;
using Class;

namespace Client.Services
{
    public class LocalStorage_Services : ILocalStorage_Services
    {

        private readonly ILocalStorageService _LocalStorage;

        public LocalStorage_Services(ILocalStorageService localStorage)
        {
            _LocalStorage = localStorage;
        }







        public async Task SetLogin(Response_Login_Request response)
        {
            await _LocalStorage.SetItemAsync(DataType.authToken.ToString(), response.Token);
            await _LocalStorage.SetItemAsync(DataType.client.ToString(), response.Client);
            await _LocalStorage.SetItemAsync(DataType.station.ToString(), response.Station);
            await _LocalStorage.SetItemAsync(DataType.user.ToString(), response.User);
        }

        public async Task RemoveLogin()
        {
            await _LocalStorage.RemoveItemAsync(DataType.authToken.ToString());
            await _LocalStorage.RemoveItemAsync(DataType.client.ToString());
            await _LocalStorage.RemoveItemAsync(DataType.station.ToString());
            await _LocalStorage.RemoveItemAsync(DataType.user.ToString());
        }


        public async Task<User_Request> GetDecodified_Login()
        {
            try
            {
                if (await _LocalStorage.GetItemAsync<User_Request>(DataType.user.ToString()) == null)
                    return null;                
                
                var user = await _LocalStorage.GetItemAsync<User_Request>(DataType.user.ToString());

                user.Name = EncodifierClass.Decodify(user.Name);
                user.Surname = EncodifierClass.Decodify(user.Surname);
                user.CompleteName = EncodifierClass.Decodify(user.Name) + " " + EncodifierClass.Decodify(user.Surname);
                user.Email = EncodifierClass.Decodify(user.Email);

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
                if (await _LocalStorage.GetItemAsync<User_Request>(DataType.user.ToString()) != null)
                {
                    var user = await _LocalStorage.GetItemAsync<User_Request>(DataType.user.ToString());

                    await _LocalStorage.RemoveItemAsync(DataType.user.ToString());

                    user.JSONListOfPermissions = EncodifierClass.Codify(permissions);
                    await _LocalStorage.SetItemAsync(DataType.user.ToString(), user);
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
                if (await _LocalStorage.GetItemAsync<User_Request>(DataType.user.ToString()) == null)
                    return null;

                var user = await _LocalStorage.GetItemAsync<User_Request>(DataType.user.ToString());
                return EncodifierClass.Decodify(user.JSONListOfPermissions);
            }
            catch (Exception)
            {
                return null;
            }
        }


    }

    internal enum DataType
    {
        client,
        station,
        user,
        authToken,
        apiurl
    }

}
