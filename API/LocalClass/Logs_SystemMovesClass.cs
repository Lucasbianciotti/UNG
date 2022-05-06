using API.Services;
using Models.EntityFrameworks;
using Models.Enums;
using Models.Request;
using System.Security.Claims;

namespace API.LocalClass
{
    public static class Logs_SystemMovesClass
    {
        #region Funciones principales

        #region Get
        public static GlobalResponse_Request CompleteInformation(ClaimsPrincipal _user, Filter_Request Filters)
        {

            var data = new GlobalResponse_Request(UsersClass.JSONListOfPermissions(_user))
            {
                ListOfSystemMoves = ListOfSystemMoves(_user, Filters),
            };

            return data;
        }
        private static List<SystemMove_Request> ListOfSystemMoves(ClaimsPrincipal _user, Filter_Request filterModel)
        {
            DateTime fechaInicial = GlobalClass.FechaInicialDeFiltro(filterModel.Date_Start);
            DateTime fechaFinal = GlobalClass.FechaFinalDeFiltro(filterModel.Date_End);

            using var db = new UNG_Context();

            try
            {
                var lista = (from mov in db.Logs_SystemMoves

                             join user in db.Users
                             on mov.IDuser equals user.ID
                             into caq
                             from user in caq.DefaultIfEmpty()

                             where
                             (
                                (mov.Date >= fechaInicial && mov.Date < fechaFinal)
                                ||
                                (mov.Date > fechaInicial && mov.Date <= fechaFinal)
                             )

                             select new SystemMove_Request
                             {
                                 ID = mov.ID,

                                 IDuser = mov.IDuser,
                                 User_Name = EncrypterService.Decodify(user.Name) + " " + EncrypterService.Decodify(user.Surname),
                                 User_Email = EncrypterService.Decodify(user.Email),

                                 Date = mov.Date,

                                 Detail = mov.Detail
                             }).OrderByDescending(x => x.Date).ToList();

                if (filterModel.IDuser != null && filterModel.IDuser.Count != 0)
                {
                    try
                    {
                        var temp = lista.Where(y => filterModel.IDuser.Any(z => z == y.IDuser)).ToList();
                        lista = temp;
                    }
                    catch (Exception)
                    { }
                }


                return lista;
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(_user,
                        new New_Error_Request()
                        {
                            Comentario = "The list was not found",
                            Excepcion = e,
                            Accion = SystemActionsEnum.SearchList,
                            Sistema = SystemTypesEnum.API,
                        });

                throw new Exception("The list was not found. " + e.Message.ToString());
            }
        }


        #endregion Get
        private static async Task Guardar(ClaimsPrincipal _user, Logs_SystemMoves model)
        {
            try
            {
                using UNG_Context db = new();
                db.Logs_SystemMoves.Add(model);
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }
        #endregion





        #region Stations
        public static async Task Create_Station(ClaimsPrincipal _user, Stations station)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDclient = station.IDclient,
                IDuser = UsersClass.GetID_User(_user),
                Detail = "Station created: \"" + station.Name + "\"",
                Aux = null,
            };

            await Guardar(_user, movimiento);
        }

        public static async Task Modify_Station(ClaimsPrincipal _user, Stations station)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDclient = station.IDclient,
                IDuser = UsersClass.GetID_User(_user),
                Detail = "Station modified: \"" + station.Name + "\"",
                Aux = null,
            };

            await Guardar(_user, movimiento);
        }


        public static async Task Delete_Station(ClaimsPrincipal _user, Stations station)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDclient = station.IDclient,
                IDuser = UsersClass.GetID_User(_user),
                Detail = "Station deleted: \"" + station.Name + "\"",
                Aux = null,
            };

            await Guardar(_user, movimiento);
        }
        #endregion



        #region Users
        public static async Task ChangePassword_User(ClaimsPrincipal _user, Users userAdmin)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDuser = UsersClass.GetID_User(_user),
                Detail = "Change password: \"" + EncrypterService.Decodify(userAdmin.Email) + "\"",
                Aux = null,
            };

            await Guardar(_user, movimiento);
        }

        public static async Task Create_User(ClaimsPrincipal _user, Users user)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDclient = UsersClass.GetID_Client(_user),
                IDuser = UsersClass.GetID_User(_user),
                Detail = "User created: \"" + EncrypterService.Decodify(user.Name) + " " + EncrypterService.Decodify(user.Surname) + "\"",
                Aux = null,
            };

            await Guardar(_user, movimiento);
        }

        public static async Task Modify_User(ClaimsPrincipal _user, Users user)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDclient = UsersClass.GetID_Client(_user),
                IDuser = UsersClass.GetID_User(_user),
                Detail = "User modified: \"" + EncrypterService.Decodify(user.Name) + " " + EncrypterService.Decodify(user.Surname) + "\"",
                Aux = null,
            };

            await Guardar(_user, movimiento);
        }

        public static async Task Delete_User(ClaimsPrincipal _user, Users user)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDclient = UsersClass.GetID_Client(_user),
                IDuser = UsersClass.GetID_User(_user),
                Detail = "User deleted: \"" + EncrypterService.Decodify(user.Name) + " " + EncrypterService.Decodify(user.Surname) + "\"",
                Aux = null,
            };

            await Guardar(_user, movimiento);
        }


        #endregion


        #region Equipment

        public static async Task Create_Equipment(ClaimsPrincipal _user, Equipments equipment)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDclient = UsersClass.GetID_Client(_user),
                IDuser = UsersClass.GetID_User(_user),
                Detail = "Equipment created: \"" + equipment.Name + "\"",
                Aux = null,
            };

            await Guardar(_user, movimiento);
        }

        public static async Task Modify_Equipment(ClaimsPrincipal _user, Equipments equipment)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDclient = UsersClass.GetID_Client(_user),
                IDuser = UsersClass.GetID_User(_user),
                Detail = "Equipment modified: \"" + equipment.Name + "\"",
                Aux = null,
            };

            await Guardar(_user, movimiento);
        }

        public static async Task Delete_Equipment(ClaimsPrincipal _user, Equipments equipment)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDclient = UsersClass.GetID_Client(_user),
                IDuser = UsersClass.GetID_User(_user),
                Detail = "Equipment deleted: \"" + equipment.Name + "\"",
                Aux = null,
            };

            await Guardar(_user, movimiento);
        }
        #endregion


    }

}
