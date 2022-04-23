using Models.Request;

namespace Web.Services
{
    public interface IGlobalElements_Services
    {
        event Action OnChange;
        public void ActualizarVista();


        public void SetearInformacion(GlobalResponse_Request temp);


        public string TituloDePagina { get; set; }
        public string Empresa { get; set; }


        public List<Dron_Request> Drones { get; set; }
        public List<Usuario_Request> Usuarios { get; set; }

        public Usuario_Request _Usuario { get; set; }

    }
}
