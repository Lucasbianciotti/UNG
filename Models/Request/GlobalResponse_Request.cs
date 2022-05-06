namespace Models.Request
{
    public class GlobalResponse_Request
    {
        public List<Client_Request> ListOfClients { get; set; }
        public List<User_Request> ListOfUsers { get; set; }


        public List<SystemMove_Request> ListOfSystemMoves { get; set; }
        public List<Equipment_Request> ListOfEquipments { get; set; }


        public InformationOfStation_Request InformationOfStations { get; set; }

        public InformationOfData_Request InformationOfData { get; set; }


        public string JSONListOfPermissions { get; set; }

        public string ObjetoJson { get; set; }

        public byte[] ObjetoByte { get; set; }


        public string Message { get; set; }


        public GlobalResponse_Request(string JSONpermissions)
        {
            JSONListOfPermissions = JSONpermissions;
        }
    }
}
