using APIClient.LocalModels.SQLite;
using Class;
using CommonModels.Enums;
using CommonModels.Request;
using System.Security.Claims;

namespace APIClient.LocalClass
{
    public static class LogsClass
    {
        #region Funciones principales

        #region Get
        public static LocalResponse_Request CompleteInformation(ClaimsPrincipal _user, Filter_Request Filtros, string URL)
        {
            var data = new LocalResponse_Request(ClientsClass.SearchClient(_user), StationsClass.SearchStation(_user, URL), UsersClass.SearchUser(_user))
            {
                ListOfMovements = ListOfMovements(_user, Filtros),
            };

            return data;
        }

        public static List<Log_Request> ListOfMovements(ClaimsPrincipal _user, Filter_Request filterModel)
        {
            using var db = new Local_Context();

            try
            {
                //filterModel.Date_Start = new DateTime(filterModel.Date_Start.Year, filterModel.Date_Start.Month, filterModel.Date_Start.Day, 0, 0, 0);
                //filterModel.Date_End = new DateTime(filterModel.Date_End.Year, filterModel.Date_End.Month, filterModel.Date_End.Day, 23, 59, 59);

                var _lista = (from data in db.Logs

                                  //where (
                                  //   (data.Datetime >= filterModel.Date_Start && data.Datetime < filterModel.Date_End)
                                  //   ||
                                  //   (data.Datetime > filterModel.Date_Start && data.Datetime <= filterModel.Date_End)
                                  //   )

                              select new Log_Request
                              {
                                  ID = data.ID,
                                  Datetime = data.Date,
                                  Detail = data.Detail,
                                  Type = data.Type
                              }).OrderBy(x => x.Datetime).ToList();

                return _lista;
            }
            catch (Exception e)
            {
                NewError(_user, "Could not load list of logs", SystemActionsEnum.SearchList, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                throw new Exception("Could not load list of logs.");
            }
        }

        #endregion Get

        private static async Task Guardar(ClaimsPrincipal _user, Logs model)
        {
            try
            {
                using Local_Context db = new();
                db.Logs.Add(model);
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }
        }
        #endregion



        #region Stations
        public static async Task Create_Station(ClaimsPrincipal _user, Stations station)
        {
            var movimiento = new Logs
            {
                Date = DateTime.Now,
                IDuser = GlobalClass.GetID_User(_user),
                Detail = "Station created: \"" + station.Name + "\"",
            };

            await Guardar(_user, movimiento);
        }

        public static async Task Modify_Station(ClaimsPrincipal _user, Stations station)
        {
            var movimiento = new Logs
            {
                Date = DateTime.Now,
                IDuser = GlobalClass.GetID_User(_user),
                Detail = "Station modified: \"" + station.Name + "\"",

            };

            await Guardar(_user, movimiento);
        }


        public static async Task Delete_Station(ClaimsPrincipal _user, Stations station)
        {
            var movimiento = new Logs
            {
                Date = DateTime.Now,
                IDuser = GlobalClass.GetID_User(_user),
                Detail = "Station deleted: \"" + station.Name + "\"",

            };

            await Guardar(_user, movimiento);
        }
        #endregion



        #region Users
        public static async Task ChangePassword_User(ClaimsPrincipal _user, Users userAdmin)
        {
            var movimiento = new Logs
            {
                Date = DateTime.Now,
                IDuser = GlobalClass.GetID_User(_user),
                Detail = "Change password: \"" + EncodifierClass.Decodify(userAdmin.Email) + "\"",

            };

            await Guardar(_user, movimiento);
        }

        public static async Task Create_User(ClaimsPrincipal _user, Users user)
        {
            var movimiento = new Logs
            {
                Date = DateTime.Now,
                IDuser = GlobalClass.GetID_User(_user),
                Detail = "User created: \"" + EncodifierClass.Decodify(user.Name) + " " + EncodifierClass.Decodify(user.Surname) + "\"",

            };

            await Guardar(_user, movimiento);
        }

        public static async Task Modify_User(ClaimsPrincipal _user, Users user)
        {
            var movimiento = new Logs
            {
                Date = DateTime.Now,
                IDuser = GlobalClass.GetID_User(_user),
                Detail = "User modified: \"" + EncodifierClass.Decodify(user.Name) + " " + EncodifierClass.Decodify(user.Surname) + "\"",

            };

            await Guardar(_user, movimiento);
        }

        public static async Task Delete_User(ClaimsPrincipal _user, Users user)
        {
            var movimiento = new Logs
            {
                Date = DateTime.Now,
                IDuser = GlobalClass.GetID_User(_user),
                Detail = "User deleted: \"" + EncodifierClass.Decodify(user.Name) + " " + EncodifierClass.Decodify(user.Surname) + "\"",

            };

            await Guardar(_user, movimiento);
        }


        #endregion



        #region Equipment

        public static async Task Create_Equipment(ClaimsPrincipal _user, Equipments equipment)
        {
            var movimiento = new Logs
            {
                Date = DateTime.Now,
                IDuser = GlobalClass.GetID_User(_user),
                Detail = "Equipment created: \"" + equipment.Name + "\"",

            };

            await Guardar(_user, movimiento);
        }

        public static async Task Modify_Equipment(ClaimsPrincipal _user, Equipments equipment)
        {
            var movimiento = new Logs
            {
                Date = DateTime.Now,
                IDuser = GlobalClass.GetID_User(_user),
                Detail = "Equipment modified: \"" + equipment.Name + "\"",

            };

            await Guardar(_user, movimiento);
        }

        public static async Task Delete_Equipment(ClaimsPrincipal _user, Equipments equipment)
        {
            var movimiento = new Logs
            {
                Date = DateTime.Now,
                IDuser = GlobalClass.GetID_User(_user),
                Detail = "Equipment deleted: \"" + equipment.Name + "\"",

            };

            await Guardar(_user, movimiento);
        }

        #endregion



        #region Clients

        internal static async Task Create_Client(ClaimsPrincipal _user, Clients client)
        {
            var movimiento = new Logs
            {
                Date = DateTime.Now,
                IDuser = GlobalClass.GetID_User(_user),
                Detail = "Client created: \"" + client.Name + "\"",

            };

            await Guardar(_user, movimiento);
        }

        #endregion



        #region Data

        internal static async Task Create_Data(ClaimsPrincipal _user, Data data)
        {
            var movimiento = new Logs
            {
                Date = DateTime.Now,
                IDuser = GlobalClass.GetID_User(_user),
                Detail = "Data received: \"" + data.Info + "\"",
            };

            await Guardar(_user, movimiento);
        }

        #endregion



        public static void NewError(ClaimsPrincipal _user, string comentario, SystemActionsEnum accion, SystemTypesEnum sistema, Exception exception, SystemErrorCodesEnum codigo)
        {
            Task.Run(async () =>
            {
                using var db = new Local_Context();

                try
                {
                    var _detail = "System: " + sistema.ToString() +
                    "Code: " + codigo.ToString() +
                    "Action: " + accion.ToString() +
                    "Error: " + exception.ToString();

                    var error = new Logs
                    {
                        IDuser = GlobalClass.GetID_User(_user),
                        Date = DateTime.Now,
                        Type = "Error",
                        Detail = _detail,
                    };

                    //if (exception != null)
                    //{
                    //    try
                    //    {
                    //        var st = new StackTrace(exception, true);
                    //        var frame = st.GetFrames()
                    //                      .Select(frame => new
                    //                      {
                    //                          FileName = frame.GetFileName(),
                    //                          LineNumber = frame.GetFileLineNumber(),
                    //                          ColumnNumber = frame.GetFileColumnNumber(),
                    //                          Method = frame.GetMethod(),
                    //                          Class = frame.GetMethod().DeclaringType,
                    //                      }).FirstOrDefault();

                    //        error.Exception = exception.ToString();
                    //        error.Exception_File = frame.FileName;
                    //        error.Exception_Method = frame.Class.ToString() + " - " + frame.Method.ToString();
                    //        error.Exception_NumberOfLine = frame.LineNumber.ToString();
                    //        error.Exception_Source = exception.Source;
                    //        if (exception.InnerException != null)
                    //            error.Exception_Message = exception.InnerException.Message.ToString();
                    //    }
                    //    catch (Exception)
                    //    {

                    //    }
                    //}

                    db.Logs.Add(error);
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {

                }

            });
        }


        public static void NewError(ClaimsPrincipal _user, New_Error_Request new_Error_Request)
        {
            NewError(_user, new_Error_Request.Comentario, new_Error_Request.Accion, new_Error_Request.Sistema, new_Error_Request.Excepcion, new_Error_Request.Codigo);
        }
    }

}
