using APIClient.LocalModels.SQLite;
using CommonModels.Enums;
using CommonModels.Global;
using CommonModels.Request;
using System.Security.Claims;

namespace APIClient.LocalClass
{
    public static class ClientsClass
    {
        public static Client_Request SearchClient(ClaimsPrincipal _user)
        {
            using var db = new Local_Context();

            try
            {
                if (db.Clients.Count() == 0)
                    return null;


                var client = db.Clients.First();
                return new Client_Request()
                {
                    ID = client.ID,
                    IDung = client.IDung,
                    IDstatus = client.IDstatus,
                    Modify_Date = client.Modify_Date,
                    Modify_IDuser = client.Modify_IDuser,
                    Name = client.Name,
                };
            }
            catch (Exception e)
            {
                LogsClass.NewError(_user, "Could not load information's client", SystemActionsEnum.SearchList, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                throw new Exception("Could not load information's client.");
            }
        }

        public static GlobalResponse Create(ClaimsPrincipal _user, Client_Request model)
        {
            using var db = new Local_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                var client = new Clients
                {
                    IDung = model.IDung,
                    IDstatus = model.IDstatus,
                    Modify_Date = model.Modify_Date,
                    Modify_IDuser = model.Modify_IDuser,
                    Name = model.Name,
                };


                db.Clients.Add(client);
                db.SaveChanges();

                transaction.Commit();


                #region Save move
                Task.Run(async () =>
                {
                    await LogsClass.Create_Client(_user, client);
                });
                #endregion Guardado de movimientos

                return new GlobalResponse(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                transaction.Rollback();

                LogsClass.NewError(_user, "Could not create the client", SystemActionsEnum.Create, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not create. " + e.Message.ToString());
            }
        }

    }
}
