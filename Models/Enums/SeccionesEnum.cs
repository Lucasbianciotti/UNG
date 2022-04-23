using System.Collections.Generic;

namespace Models.Enums
{
    public class SeccionesEnum
    {
        public static readonly List<string> Secciones = new()
        {
            "Dashboard",
            "Drones",
            "Configuracion",
            "Usuarios",
        };

        #region Secciones
        public static string Dashboard { get { return "Dashboard"; } }

        public static string Ventas_Presupuestos { get { return "Drones"; } }

        public static string Configuracion { get { return "Configuracion"; } }

        public static string Usuarios { get { return "Usuarios"; } }
        #endregion

    }
}
