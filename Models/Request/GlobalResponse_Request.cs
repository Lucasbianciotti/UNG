namespace Models.Request
{
    public class GlobalResponse_Request
    {
        public List<User_Request> Users { get; set; }

        public List<Equipment_Request> List_Drone { get; set; }
        public List<SystemMove_Request> List_SystemMoves { get; set; }



        public string ObjetoJson { get; set; }

        public byte[] ObjetoByte { get; set; }


        public string _Message { get; set; }


        public GlobalResponse_Request()
        {

        }
    }
}
