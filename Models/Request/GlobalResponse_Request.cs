namespace Models.Request
{
    public class GlobalResponse_Request
    {
        public List<Usuario_Request> Usuarios { get; set; }

        public List<Dron_Request> Drones { get; set; }
        public List<MovimientosDelSistema_Request> ListaDeMovimientosDelSistema { get; set; }



        public string ObjetoJson { get; set; }

        public byte[] ObjetoByte { get; set; }


        public string _Mensaje { get; set; }


        public GlobalResponse_Request()
        {

        }
    }
}
