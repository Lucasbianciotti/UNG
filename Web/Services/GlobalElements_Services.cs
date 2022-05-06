using Models.Request;

namespace Web.Services
{
    public class GlobalElements_Services : IGlobalElements_Services
    {
        private readonly ILocalStorage_Services _LocalStorage;

        public GlobalElements_Services(ILocalStorage_Services localStorage)
        {
            _LocalStorage = localStorage;

            TitleOfPage = "UNG system";
            Client = "";

            Task.Run(async () =>
            {
                User = await _LocalStorage.GetDecodified_Login();
            });

            PermissionForSection = new();


            InformationOfStations = new();
            InformationOfData = new();
        }



        public void SetearInformacion(GlobalResponse_Request temp)
        {
            if (temp == null)
                return;

            Task.Run(async () =>
            {
                await _LocalStorage.SetCodified_JSONPermission(temp.JSONListOfPermissions);
            });

            if (temp.InformationOfStations != null)
            {
                InformationOfStations = temp.InformationOfStations;
                //Drones.Total = temp.Total;
                //Drones.Cantidad = temp.Cantidad;
                //Drones.ACobrar = temp.ACobrar_APagar;
                //Drones.Cobrado = temp.Cobrado_Pagado;
            }

            if (temp.InformationOfData != null)
            {
                InformationOfData = temp.InformationOfData;
                //Drones.Total = temp.Total;
                //Drones.Cantidad = temp.Cantidad;
                //Drones.ACobrar = temp.ACobrar_APagar;
                //Drones.Cobrado = temp.Cobrado_Pagado;
            }

            //if (temp.ListOfUsers != null)
            //    ListOfUsers = temp.ListOfUsers;

            //ActualizarVista();
        }



        #region Listas
        public InformationOfStation_Request InformationOfStations { get; set; }
        public InformationOfData_Request InformationOfData { get; set; }
        public Permissions_Request PermissionForSection { get; set; }
        public User_Request User { get; set; }
        #endregion Listas


        #region Variables
        public string TitleOfPage { get; set; }
        public string Client { get; set; }

        #endregion Variables


    }

}
