using Models.Request;

namespace Web.Services
{
    public class GlobalElements_Services : IGlobalElements_Services
    {

        public GlobalElements_Services()
        {
            TituloDePagina = "UNG system";
            Empresa = "";

            _Usuario = null;

            Drones = new();
            Users = new();
        }



        public void SetearInformacion(GlobalResponse_Request temp)
        {
            if (temp == null)
                return;

            if (temp.List_Drone != null)
            {
                Drones.List_Drone = temp.List_Drone;
                //Drones.Total = temp.Total;
                //Drones.Cantidad = temp.Cantidad;
                //Drones.ACobrar = temp.ACobrar_APagar;
                //Drones.Cobrado = temp.Cobrado_Pagado;
            }

            if (temp.Users != null)
                Users = temp.Users;

            ActualizarVista();
        }


        #region Listas
        public InformacionDeDrones_Request Drones { get; set; }
        public User_Request _Usuario { get; set; }
        public List<User_Request> Users { get; set; }


        #endregion Listas


        #region Variables
        public string TituloDePagina { get; set; }
        public string Empresa { get; set; }


        private bool _Modal_ImprimirFactura_Visible;
        public bool Modal_ImprimirFactura_Visible
        {
            get
            {
                return _Modal_ImprimirFactura_Visible;
            }
            set
            {
                _Modal_ImprimirFactura_Visible = value;
                ActualizarVista();
            }
        }

        #endregion Variables


        public event Action OnChange;
        public void ActualizarVista()
        {
            OnChange?.Invoke();
        }

    }

}
