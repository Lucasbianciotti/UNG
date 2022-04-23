using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class MovimientosDelSistema_Request
    {
        public MovimientosDelSistema_Request()
        {
            Filtros = new Filter_Request();
        }

        public Filter_Request Filtros { get; set; }


        [Key]
        public long ID { get; set; }

        public DateTime Fecha { get; set; }

        public long IDusuario { get; set; }
        public string Usuario_Nombre { get; set; }
        public string Usuario_Email { get; set; }


        public string Detalle { get; set; }

    }
}
