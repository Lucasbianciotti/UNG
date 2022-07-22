using CommonModels.Request;

namespace Client.Services
{
    public interface IGlobalElements_Services
    {
        public void SetearInformacion(LocalResponse_Request temp);

        public string TitleOfPage { get; set; }

        public Client_Request Client { get; set; }
        public Station_Request Station { get; set; }
        public User_Request User { get; set; }


        public List<Data_Request> ListOfData { get; set; }

        public List<Log_Request> ListOfMovements { get; set; }

        public Permissions_Request PermissionForSection { get; set; }
    }
}
