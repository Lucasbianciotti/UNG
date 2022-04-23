using Models.Enums;

namespace Models.Request
{
    public class New_Error_Request
    {
        public Exception Excepcion { get; set; }
        public string Excepcion_Source { get; set; }
        public string Excepcion_Mensaje { get; set; }
        public string Excepcion_Metodo { get; set; }
        public string Excepcion_Archivo { get; set; }
        public int Excepcion_NumeroDeLinea { get; set; }


        public AccionesDelSistemaEnum Accion { get; set; }
        public string Comentario { get; set; }
        public string Aux { get; set; }
        public TiposDeSistemaEnum Sistema { get; set; }
        public CodigosDeErrorEnum Codigo { get; set; }
    }
}
