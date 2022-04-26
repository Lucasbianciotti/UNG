using API.LocalClass;
using API.Services;
using Models.EntityFrameworks;
using Models.Enums;
using Models.Request;
using System.Security.Claims;

namespace API.LocalClass
{
    public static class Logs_MovimientosDelSistemaClass
    {
        #region Funciones principales

        #region Get
        public static GlobalResponse_Request InformacionCompleta(ClaimsPrincipal User, Filter_Request Filtros)
        {

            var data = new GlobalResponse_Request()
            {
                List_SystemMoves = ListaDeMovimientos(User, Filtros),
                Users = UsersClass.ListaDeUsers(User),
            };

            return data;
        }
        private static List<SystemMove_Request> ListaDeMovimientos(ClaimsPrincipal user, Filter_Request filterModel)
        {
            DateTime fechaInicial = GlobalClass.FechaInicialDeFiltro(filterModel.Date_Start);
            DateTime fechaFinal = GlobalClass.FechaFinalDeFiltro(filterModel.Date_End);

            using var db = new UNG_Context();

            try
            {
                var lista = (from mov in db.Logs_SystemMoves

                             join usuario in db.Users
                             on mov.IDuser equals usuario.ID
                             into caq
                             from usuario in caq.DefaultIfEmpty()

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
                                 User_Name = EncrypterService.Decodify(usuario.Name) + " " + EncrypterService.Decodify(usuario.Surname),
                                 User_Email = EncrypterService.Decodify(usuario.Email),

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
                Logs_ErroresClass.NuevoLog(user,
                        new New_Error_Request()
                        {
                            Comentario = "The list was not found",
                            Excepcion = e,
                            Accion = AccionesDelSistemaEnum.BuscarLista,
                            Sistema = TiposDeSistemaEnum.API,
                        });

                throw new Exception("The list was not found. " + e.Message.ToString());
            }
        }


        #endregion Get

        private static async Task Guardar(ClaimsPrincipal user, Logs_SystemMoves model)
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



        #region Users
        public static async Task CambiarContraseña_Usuario(ClaimsPrincipal user, Users usuarioAdmin)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDuser = UsersClass.GetID_User(user),
                Detail = "Se CAMBIÓ LA CONTRASEÑA de usuario: \"" + EncrypterService.Decodify(usuarioAdmin.Email) + "\"",
                Aux = null,
            };

            await Guardar(user, movimiento);
        }

        public static async Task Crear_Usuario(ClaimsPrincipal user, Users usuario)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDcompany = usuario.IDcompany,
                IDuser = UsersClass.GetID_User(user),
                Detail = "Se creó el USUARIO: \"" + EncrypterService.Decodify(usuario.Name) + " " + EncrypterService.Decodify(usuario.Surname) + "\"",
                Aux = null,
            };

            await Guardar(user, movimiento);
        }

        public static async Task Modificar_Usuario(ClaimsPrincipal user, Users usuario)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDcompany = usuario.IDcompany,
                IDuser = UsersClass.GetID_User(user),
                Detail = "Se modificó el USUARIO: \"" + EncrypterService.Decodify(usuario.Name) + " " + EncrypterService.Decodify(usuario.Surname) + "\"",
                Aux = null,
            };

            await Guardar(user, movimiento);
        }

        public static async Task Eliminar_Usuario(ClaimsPrincipal user, Users usuario)
        {
            var movimiento = new Logs_SystemMoves
            {
                Date = DateTime.Now,

                IDcompany = usuario.IDcompany,
                IDuser = UsersClass.GetID_User(user),
                Detail = "Se eliminó el USUARIO: \"" + EncrypterService.Decodify(usuario.Name) + " " + EncrypterService.Decodify(usuario.Surname) + "\"",
                Aux = null,
            };

            await Guardar(user, movimiento);
        }
        #endregion

    }

}
