namespace Models
{
    public static class URLs
    {
        public readonly static string _API = "https://localhost:7212/";
        //public readonly static string _API = "http://192.168.120.18:7213/";//API IN IIS
        //public readonly static string _API = "https://ungsystemapitest.cloudnetsolutions.com.ar/";
        //public readonly static string _API = "https://apiungsystem.cloudnetsolutions.com.ar/";

        public readonly static string ReportSignal = _API + "disparohub";

        #region Dashboard
        public readonly static string Dashboard = _API + "api/dashboard/index";
        #endregion

        #region Stations
        public readonly static string Create_Station = _API + "api/Stations/Create";
        public readonly static string Modify_Station = _API + "api/Stations/Modify";
        public readonly static string Delete_Station = _API + "api/Stations/Delete";
        public readonly static string Stations = _API + "api/Stations/index";
        #endregion

        #region Stations
        public readonly static string Create_Equipment = _API + "api/Equipments/Create";
        public readonly static string Modify_Equipment = _API + "api/Equipments/Modify";
        public readonly static string Delete_Equipment = _API + "api/Equipments/Delete";
        public readonly static string EquipmentsOfStation = _API + "api/Equipments/EquipmentsOfStation";
        //public readonly static string Equipments = _API + "api/Equipments/index";
        #endregion

        #region Data
        public readonly static string Data = _API + "api/Data/index";
        #endregion

        #region User
        public readonly static string CambiarPassword_User = _API + "api/Users/ChangePassword";
        public readonly static string Create_User = _API + "api/Users/Create";
        public readonly static string Modify_User = _API + "api/Users/Modify";
        public readonly static string Delete_User = _API + "api/Users/Delete";
        public readonly static string Users = _API + "api/Users/index";
        #endregion

        #region Movimientos del sistema
        public readonly static string MovimientosDelSistema = _API + "api/MovimientosDelSistema/index";
        #endregion

        #region Login
        public readonly static string Login = _API + "api/Users/Login";
        public readonly static string Login_ReestablecerPassword = _API + "api/Users/Login_ReestablecerContrasena";
        public readonly static string Login_ActualizarPassword = _API + "api/Users/Login_ActualizarContrasena";
        #endregion

        #region Errores
        public readonly static string Errores = _API + "api/ErroresDelSistema/Nuevo";
        #endregion
    }

}
