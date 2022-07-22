namespace Client.Services
{
    public interface IURLs_Services
    {
        public Task SetURLAPI(string url);
        public Task<string> GetURLAPI();


        public Task<string> Test();
        public Task<string> ReportSignal();

        #region Dashboard
        public Task<string> Dashboard();
        #endregion

        #region Stations
        public Task<string> Create_Station();
        public Task<string> Modify_Station();
        public Task<string> Delete_Station();
        public Task<string> SearchStation();
        #endregion

        #region Equipment
        public Task<string> Create_Equipment();
        public Task<string> Modify_Equipment();
        public Task<string> Delete_Equipment();
        public Task<string> EquipmentsOfStation();
        #endregion

        #region Data
        public Task<string> Data();
        public Task<string> Delete_AllData();
        public Task<string> Recreate_Tables();
        #endregion

        #region User
        public Task<string> CambiarPassword_User();
        public Task<string> Create_User();
        public Task<string> Modify_User();
        public Task<string> Delete_User();
        public Task<string> Users();
        #endregion

        #region Movimientos del sistema
        public Task<string> MovimientosDelSistema();

        #endregion

        #region Login
        public Task<string> SignUp();
        public Task<string> Login();
        public Task<string> Login_RestorePassword();
        public Task<string> Login_UpdatePassword();
        #endregion

        #region Logs
        public Task<string> Logs();
        #endregion

        #region Errores
        public Task<string> Errores();
        #endregion
    }
}
