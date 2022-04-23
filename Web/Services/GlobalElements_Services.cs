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
            Usuarios = new();
        }



        public void SetearInformacion(GlobalResponse_Request temp)
        {
            if (temp == null)
                return;

            if (temp.Drones != null)
                Drones = temp.Drones;

            if (temp.Usuarios != null)
                Usuarios = temp.Usuarios;

            ActualizarVista();
        }


        #region Listas
        public List<Dron_Request> Drones { get; set; }
        public Usuario_Request _Usuario { get; set; }
        public List<Usuario_Request> Usuarios { get; set; }


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
