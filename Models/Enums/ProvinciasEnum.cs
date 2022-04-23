using Models.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Enums
{
    public static class ProvinciasEnum
    {
        public static readonly List<string> Provincias = new List<string>()
        {
            "Buenos Aires",
            "Catamarca",
            "Chaco",
            "Chubut",
            "Córdoba",
            "Corrientes",
            "Entre Ríos",
            "Formosa",
            "Jujuy",
            "La Pampa",
            "La Rioja",
            "Mendoza",
            "Misiones",
            "Neuquén",
            "Río Negro",
            "Salta",
            "San Juan",
            "Santa Cruz",
            "Santa Fe",
            "Santiago del Estero",
            "Tierra del Fuego",
            "Tucumán"
        };

        public static List<xEditableItem> ListEditableItem_Provincias()
        {
            var tipos = Provincias;
            List<xEditableItem> items = new List<xEditableItem>();

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
