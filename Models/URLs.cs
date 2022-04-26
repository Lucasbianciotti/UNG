namespace Models
{
    public static class URLs
    {
        public readonly static string _API = "https://localhost:7212/";
        //public readonly static string _API = "https://ungsystemapitest.cloudnetsolutions.com.ar/";
        //public readonly static string _API = "https://apiungsystem.cloudnetsolutions.com.ar/";


        #region Dashboard
        public readonly static string Dashboard = _API + "api/dashboard/index";
        #endregion

        #region Dron
        public readonly static string Crear_Dron = _API + "api/Drones/Crear";
        public readonly static string Modificar_Dron = _API + "api/Drones/Modificar";
        public readonly static string Eliminar_Dron = _API + "api/Drones/Eliminar";
        public readonly static string Drones = _API + "api/Drones/index";
        #endregion

        #region Usuario
        public readonly static string CambiarPassword_Usuario = _API + "api/Users/CambiarContrasena";
        public readonly static string Crear_Usuario = _API + "api/Users/Crear";
        public readonly static string Modificar_Usuario = _API + "api/Users/Modificar";
        public readonly static string Eliminar_Usuario = _API + "api/Users/Eliminar";
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
