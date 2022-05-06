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


        public SystemActionsEnum Accion { get; set; }
        public string Comentario { get; set; }
        public string Aux { get; set; }
        public SystemTypesEnum Sistema { get; set; }
        public SystemErrorCodesEnum Codigo { get; set; }
    }
}
