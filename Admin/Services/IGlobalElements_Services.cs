using Models.Request;

namespace Admin.Services
{
    public interface IGlobalElements_Services
    {
        public void SetearInformacion(GlobalResponse_Request temp);

        public string TitleOfPage { get; set; }
        public string Client { get; set; }


        public InformationOfStation_Request InformationOfStations { get; set; }
        public InformationOfData_Request InformationOfData { get; set; }
        public Permissions_Request PermissionForSection { get; set; }
        public User_Request User { get; set; }
    }
}
