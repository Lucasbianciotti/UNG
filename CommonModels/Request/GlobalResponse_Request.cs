namespace Models.Request
{
    //public class GlobalResponse_Request
    //{
    //    public List<Client_Request> ListOfClients { get; set; }
    //    public List<User_Request> ListOfUsers { get; set; }


    //    public List<SystemMove_Request> ListOfSystemMoves { get; set; }
    //    //public List<Equipment_Request> ListOfEquipments { get; set; }


    //    public Station_Request Station { get; set; }
    //    //public InformationOfStation_Request InformationOfStations { get; set; }

    //    public InformationOfData_Request InformationOfData { get; set; }


    //    public string JSONListOfPermissions { get; set; }

    //    public string ObjetoJson { get; set; }

    //    public byte[] ObjetoByte { get; set; }


    //    public string Message { get; set; }


    //    public GlobalResponse_Request(string JSONpermissions)
    //    {
    //        JSONListOfPermissions = JSONpermissions;
    //    }
    //}

    public class LocalResponse_Request
    {
        public List<Data_Request> ListOfData { get; set; }
        public List<Equipment_Request> ListOfEquipments { get; set; }
        public List<User_Request> ListOfUsers { get; set; }
        public List<SystemMove_Request> ListOfSystemMoves { get; set; }


        public Client_Request Client { get; set; }
        public Station_Request Station { get; set; }
        public User_Request User { get; set; }


        public string ObjetoJson { get; set; }

        public string Message { get; set; }


        public LocalResponse_Request(Client_Request _client, Station_Request _station, User_Request _user)
        {
            Station = _station;
            Client = _client;
            User = _user;
        }
    }

}
