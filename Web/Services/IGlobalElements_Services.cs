using Models.Request;

namespace Web.Services
{
    public interface IGlobalElements_Services
    {
        event Action OnChange;
        public void ActualizarVista();


        public void SetearInformacion(GlobalResponse_Request temp);


        public string TituloDePagina { get; set; }
        public string Company { get; set; }


        public InformacionDeDrones_Request Drones { get; set; }

        public List<User_Request> Users { get; set; }

        public User_Request _Usuario { get; set; }

    }
}
