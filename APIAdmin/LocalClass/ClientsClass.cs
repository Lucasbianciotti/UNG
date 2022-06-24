using APIAdmin.LocalModels.EntityFrameworks;
using Models.Enums;
using Models.Request;
using System.Security.Claims;

namespace APIAdmin.LocalClass
{
    public static class ClientsClass
    {
        public static Clients SearchClientOfUser(Users user)
        {
            using var db = new UNG_Context();

            try
            {
                return db.Clients.Find(user.IDclient);
            }
            catch (Exception)
            {
                throw new Exception("The Client was not found.");
            }
        }

        internal static List<Client_Request> ListOfClients(ClaimsPrincipal user, Filter_Request filtros)
        {
            if (RolesEnum.Admin == 0)
            {
                return new List<Client_Request>();
            }
            else
            {
                return new List<Client_Request>();
            }
        }
    }
}
