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

        #region Usuario
        public readonly static string CambiarContraseña_Usuario = _API + "api/Usuarios/CambiarContrasena";
        public readonly static string Crear_Usuario = _API + "api/Usuarios/Crear";
        public readonly static string Modificar_Usuario = _API + "api/Usuarios/Modificar";
        public readonly static string Eliminar_Usuario = _API + "api/Usuarios/Eliminar";
        public readonly static string Usuarios = _API + "api/Usuarios/index";
        #endregion

        #region Movimientos del sistema
        public readonly static string MovimientosDelSistema = _API + "api/MovimientosDelSistema/index";
        #endregion

        #region Login
        public readonly static string Login = _API + "api/Usuarios/Login";
        public readonly static string Login_ReestablecerContraseña = _API + "api/Usuarios/Login_ReestablecerContrasena";
        public readonly static string Login_ActualizarContraseña = _API + "api/Usuarios/Login_ActualizarContrasena";
        #endregion

        #region Errores
        public readonly static string Errores = _API + "api/ErroresDelSistema/Nuevo";
        #endregion
    }

}
