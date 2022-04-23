using System.Collections.Generic;

namespace Models.Enums
{
    public class RolDeUsuarioEnUnidadDeNegocio_Request
    {
        public long IDunidad { get; set; }
        public string Rol { get; set; }
    }
    public static class RolesDeUsuariosEnum
    {

        public static readonly List<string> Tipos = new List<string>()
        {
            "Administrador",
            "Supervisor",
        };

        //public static string AdministradorGeneral { get { return "AdministradorGeneral"; } }
        //public static string Administrador { get { return "Administrador"; } }
        //public static string Usuario { get { return "Usuario"; } }
        //public static string Contador { get { return "Contador"; } }
        //public static string Comprador { get { return "Comprador"; } }
        //public static string Vendedor { get { return "Vendedor"; } }
    }
}
