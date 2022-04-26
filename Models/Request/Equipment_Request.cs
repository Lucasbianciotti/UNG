using Models.Global;
using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class InformacionDeDrones_Request
    {
        public List<xEditableItem> Status_Drone { get; set; }

        public Filter_Request Filtros { get; set; }

        public List<Equipment_Request> List_Drone { get; set; }

        public string Cobrado { get; set; }
        public string ACobrar { get; set; }
        public string Total { get; set; }
        public string Cantidad { get; set; }

        public InformacionDeDrones_Request()
        {
            Cobrado = "-";
            ACobrar = "-";
            Total = "-";
            Cantidad = "-";

            Filtros = new();

            //EstadosDeDrones = EstadosDeVentasEnum.ListEditableItem_EstadosDeVentas();
        }
    }

    public class Equipment_Request
    {
        public Equipment_Request()
        {
            Filtros = new();
        }

        [Key]
        public long ID { get; set; }

        public Filter_Request Filtros { get; set; }



        public string Aux { get; set; }
    }

    public class EliminarDron_Request
    {
        [Required]
        public long ID { get; set; }
    }
}
