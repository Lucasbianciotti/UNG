using System.ComponentModel.DataAnnotations;

namespace CommonModels.Global
{
    public class xEditableItem
    {
        [Key]
        public long Value { get; set; }
        public string Text { get; set; }

        public string ValueAux { get; set; }
        public string TextAux { get; set; }

        public xEditableItem()
        {
            Value = 0;
            ValueAux = "0";
            Text = "Seleccione una opción";
            TextAux = "Seleccione una opción";
        }
    }
}
