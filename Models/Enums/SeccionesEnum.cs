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
            "Users",
        };

        #region Secciones
        public static string Dashboard { get { return "Dashboard"; } }

        public static string Lecturas { get { return "Lecturas"; } }
        public static string Drones { get { return "Drones"; } }

        public static string Configuracion { get { return "Configuracion"; } }

        public static string Users { get { return "Users"; } }
        #endregion

    }
}
