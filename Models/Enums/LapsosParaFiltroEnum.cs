using Models.Global;
using Models.Request;

namespace Models.Enums
{
    public class LapsosParaFiltroEnum
    {
        public static List<xEditableItem> ListEditableItem_LapsosParaFiltro()
        {
            List<xEditableItem> items = new()
            {
                new xEditableItem
                {
                    Text = "Día",
                    ValueAux = LapsosParaFiltro_Dashboard.Dia
                },
                new xEditableItem
                {
                    Text = "Semana",
                    ValueAux = LapsosParaFiltro_Dashboard.Semana
                },
                new xEditableItem
                {
                    Text = "Mes",
                    ValueAux = LapsosParaFiltro_Dashboard.Mes
                },
                new xEditableItem
                {
                    Text = "Año",
                    ValueAux = LapsosParaFiltro_Dashboard.Año
                }
            };

            return items;
        }

    }

    public static class RepeticionesAnualesEnum
    {
        public static readonly List<string> Repeticiones = new()
        {
            "30 días",
            "60 días",
            "90 días",
            "1 año",
            "Otro",
            //"Personalizado"
        };

        public static string TreintaDias { get { return "30 días"; } }
        public static string SesentaDias { get { return "60 días"; } }
        public static string NoventaDias { get { return "90 días"; } }
        public static string UnAño { get { return "1 año"; } }
        public static string Otro { get { return "Otro"; } }

        public static List<xEditableItem> ListEditableItem_Repeticiones()
        {
            var tipos = Repeticiones;
            List<xEditableItem> items = new();

            foreach (var i in tipos)
            {
                items.Add(new xEditableItem
                {
                    Text = i,
                    ValueAux = i
                });
            }

            return items;
        }
    }


    public static class RepeticionesMensualesEnum
    {
        public static readonly List<string> Repeticiones = new()
        {
            "Número de veces",
            "Siempre",
            "Hasta",
            //"Personalizado"
        };

        public static string Siempre { get { return "Siempre"; } }
        public static string NumeroDeVeces { get { return "Número de veces"; } }
        public static string Hasta { get { return "Hasta"; } }
        public static string Personalizado { get { return "Personalizado"; } }

        public static List<xEditableItem> ListEditableItem_Repeticiones()
        {
            var tipos = Repeticiones;
            List<xEditableItem> items = new();

            foreach (var i in tipos)
            {
                items.Add(new xEditableItem
                {
                    Text = i,
                    ValueAux = i
                });
            }

            return items;
        }
    }

}
