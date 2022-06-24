using Models.Request;

namespace Client.Services
{
    public class GlobalElements_Services : IGlobalElements_Services
    {
        private readonly ILocalStorage_Services _LocalStorage;

        public GlobalElements_Services(ILocalStorage_Services localStorage)
        {
            _LocalStorage = localStorage;

            TitleOfPage = "UNG system";

            PermissionForSection = new();

            Client = new();
            Station = new();
            User = new();
            ListOfData = new();
        }



        public void SetearInformacion(LocalResponse_Request temp)
        {
            if (temp == null)
                return;

            Task.Run(async () =>
            {
                await _LocalStorage.SetCodified_JSONPermission(temp.User.JSONListOfPermissions);
            });

            if (temp.Client != null)
                Client = temp.Client;

            if (temp.Station != null)
                Station = temp.Station;

            if (temp.User != null)
                User = temp.User;

            if (temp.ListOfData != null)
                ListOfData = temp.ListOfData;

            //ActualizarVista();
        }



        #region Listas
        public List<Data_Request> ListOfData { get; set; }
        public Permissions_Request PermissionForSection { get; set; }
        public Client_Request Client { get; set; }
        public Station_Request Station { get; set; }
        public User_Request User { get; set; }
        #endregion Listas


        #region Variables
        public string TitleOfPage { get; set; }
        #endregion Variables


    }

}
