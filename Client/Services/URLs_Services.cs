using Blazored.LocalStorage;

namespace Client.Services
{
    public class URLs_Services : IURLs_Services
    {



        private readonly ILocalStorageService _LocalStorage;

        public URLs_Services(ILocalStorageService localStorage)
        {
            _LocalStorage = localStorage;
        }



        public async Task SetURLAPI(string url)
        {
            await _LocalStorage.SetItemAsync(DataType.apiurl.ToString(), url);
        }

        public async Task<string> GetURLAPI()
        {
            var url = await _LocalStorage.GetItemAsync<string>(DataType.apiurl.ToString());

            if (string.IsNullOrEmpty(url))
                return "https://localhost:5001/";

            return url;
        }



        public async Task<string> Test() { return await GetURLAPI(); }
        public async Task<string> ReportSignal() { return await GetURLAPI() + "signalr/disparohub"; }

        #region Dashboard
        public async Task<string> Dashboard() { return await GetURLAPI() + "api/dashboard/index"; }
        #endregion

        #region Stations
        public async Task<string> Create_Station() { return await GetURLAPI() + "api/Stations/Create"; }
        public async Task<string> Modify_Station() { return await GetURLAPI() + "api/Stations/Modify"; }
        public async Task<string> Delete_Station() { return await GetURLAPI() + "api/Stations/Delete"; }
        public async Task<string> SearchStation() { return await GetURLAPI() + "api/Stations/SearchStation"; }
        #endregion

        #region Equipment
        public async Task<string> Create_Equipment() { return await GetURLAPI() + "api/Equipments/Create"; }
        public async Task<string> Modify_Equipment() { return await GetURLAPI() + "api/Equipments/Modify"; }
        public async Task<string> Delete_Equipment() { return await GetURLAPI() + "api/Equipments/Delete"; }
        public async Task<string> EquipmentsOfStation() { return await GetURLAPI() + "api/Equipments/EquipmentsOfStation"; }
        #endregion

        #region Data
        public async Task<string> Data() { return await GetURLAPI() + "api/Data/index"; }
        public async Task<string> Delete_Data() { return await GetURLAPI() + "api/Data/Delete"; }
        public async Task<string> Recreate_Tables() { return await GetURLAPI() + "api/Data/RecreateTables"; }
        #endregion

        #region User
        public async Task<string> CambiarPassword_User() { return await GetURLAPI() + "api/Users/ChangePassword"; }
        public async Task<string> Create_User() { return await GetURLAPI() + "api/Users/Create"; }
        public async Task<string> Modify_User() { return await GetURLAPI() + "api/Users/Modify"; }
        public async Task<string> Delete_User() { return await GetURLAPI() + "api/Users/Delete"; }
        public async Task<string> Users() { return await GetURLAPI() + "api/Users/index"; }
        #endregion

        #region Movimientos del sistema
        public async Task<string> MovimientosDelSistema() { return await GetURLAPI() + "api/MovimientosDelSistema/index"; }

        #endregion

        #region Login
        public async Task<string> SignUp() { return await GetURLAPI() + "api/Users/SignUp"; }
        public async Task<string> Login() { return await GetURLAPI() + "api/Users/Login"; }
        public async Task<string> Login_RestorePassword() { return await GetURLAPI() + "api/Users/Login_RestorePassword"; }
        public async Task<string> Login_UpdatePassword() { return await GetURLAPI() + "api/Users/Login_UpdatePassword"; }
        #endregion

        #region Errores
        public async Task<string> Errores() { return await GetURLAPI() + "api/ErroresDelSistema/Nuevo"; }
        #endregion

    }
}
