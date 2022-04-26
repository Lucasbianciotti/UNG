using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public interface _Global_Model
    {
        [Key]
        public long ID { get; set; }


        //public string IDstatus { get; set; }

        public DateTime Fecha_Creacion { get; set; }


        //public Usuario_Request Usuario2 { get; set; }


        public string Aux { get; set; }

    }
}
