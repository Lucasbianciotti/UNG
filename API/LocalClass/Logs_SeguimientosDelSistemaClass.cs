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
                ListaDeMovimientosDelSistema = ListaDeMovimientos(User, Filtros),
                Usuarios = UsuariosClass.ListaDeUsuarios(User),
            };

            return data;
        }
        private static List<MovimientosDelSistema_Request> ListaDeMovimientos(ClaimsPrincipal user, Filter_Request filterModel)
        {
            DateTime fechaInicial = GlobalClass.FechaInicialDeFiltro(filterModel.Fecha_Inicio);
            DateTime fechaFinal = GlobalClass.FechaFinalDeFiltro(filterModel.Fecha_Final);

            using var db = new UNG_Context();

            try
            {
                var lista = (from mov in db.Logs_MovimientosDelSistema

                             join usuario in db.Usuarios
                             on mov.IDusuario equals usuario.ID
                             into caq
                             from usuario in caq.DefaultIfEmpty()

                             where
                             (
                                (mov.Fecha >= fechaInicial && mov.Fecha < fechaFinal)
                                ||
                                (mov.Fecha > fechaInicial && mov.Fecha <= fechaFinal)
                             )

                             select new MovimientosDelSistema_Request
                             {
                                 ID = mov.ID,

                                 IDusuario = mov.IDusuario,
                                 Usuario_Nombre = EncrypterService.Decodify(usuario.Nombre) + " " + EncrypterService.Decodify(usuario.Apellido),
                                 Usuario_Email = EncrypterService.Decodify(usuario.Email),

                                 Fecha = mov.Fecha,

                                 Detalle = mov.Detalle,
                             }).OrderByDescending(x => x.Fecha).ToList();

                if (filterModel.IDusuario != null && filterModel.IDusuario.Count != 0)
                {
                    try
                    {
                        var temp = lista.Where(y => filterModel.IDusuario.Any(z => z == y.IDusuario)).ToList();
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
                            Comentario = "No se pudo buscar la lista de movimientos del sistema",
                            Excepcion = e,
                            Accion = AccionesDelSistemaEnum.BuscarLista,
                            Sistema = TiposDeSistemaEnum.API,
                        });

                throw new Exception("No se pudo buscar la lista de movimientos. " + e.Message.ToString());
            }
        }


        #endregion Get

        private static async Task Guardar(ClaimsPrincipal user, Logs_MovimientosDelSistema model)
        {
            try
            {
                using UNG_Context db = new();
                db.Logs_MovimientosDelSistema.Add(model);
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        #endregion



        #region Usuarios
        public static async Task CambiarContraseña_Usuario(ClaimsPrincipal user, Usuarios usuarioAdmin)
        {
            var movimiento = new Logs_MovimientosDelSistema
            {
                Fecha = DateTime.Now,

                IDusuario = UsuariosClass.GetID_User(user),
                Detalle = "Se CAMBIÓ LA CONTRASEÑA de usuario: \"" + EncrypterService.Decodify(usuarioAdmin.Email) + "\"",
                Aux = null,
            };

            await Guardar(user, movimiento);
        }

        public static async Task Crear_Usuario(ClaimsPrincipal user, Usuarios usuario)
        {
            var movimiento = new Logs_MovimientosDelSistema
            {
                Fecha = DateTime.Now,

                IDempresa = usuario.IDempresa,
                IDusuario = UsuariosClass.GetID_User(user),
                Detalle = "Se creó el USUARIO: \"" + EncrypterService.Decodify(usuario.Nombre) + " " + EncrypterService.Decodify(usuario.Apellido) + "\"",
                Aux = null,
            };

            await Guardar(user, movimiento);
        }

        public static async Task Modificar_Usuario(ClaimsPrincipal user, Usuarios usuario)
        {
            var movimiento = new Logs_MovimientosDelSistema
            {
                Fecha = DateTime.Now,

                IDempresa = usuario.IDempresa,
                IDusuario = UsuariosClass.GetID_User(user),
                Detalle = "Se modificó el USUARIO: \"" + EncrypterService.Decodify(usuario.Nombre) + " " + EncrypterService.Decodify(usuario.Apellido) + "\"",
                Aux = null,
            };

            await Guardar(user, movimiento);
        }

        public static async Task Eliminar_Usuario(ClaimsPrincipal user, Usuarios usuario)
        {
            var movimiento = new Logs_MovimientosDelSistema
            {
                Fecha = DateTime.Now,

                IDempresa = usuario.IDempresa,
                IDusuario = UsuariosClass.GetID_User(user),
                Detalle = "Se eliminó el USUARIO: \"" + EncrypterService.Decodify(usuario.Nombre) + " " + EncrypterService.Decodify(usuario.Apellido) + "\"",
                Aux = null,
            };

            await Guardar(user, movimiento);
        }
        #endregion

    }

}
