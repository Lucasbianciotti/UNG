using API.Services;
using Models.EntityFrameworks;
using Models.Enums;
using Models.Global;
using Models.Request;
using System.Security.Claims;

namespace API.LocalClass
{
    public static class UsersClass
    {

        #region Get
        public static GlobalResponse_Request InformacionCompleta(ClaimsPrincipal user)
        {

            var data = new GlobalResponse_Request()
            {
                Users = ListaDeUsers(user),
            };

            return data;
        }

        public static GlobalResponse_Request InformacionParcial(ClaimsPrincipal user)
        {

            var data = new GlobalResponse_Request()
            {
                Users = ListaDeUsers(user),
            };

            return data;
        }


        public static User_Request BuscarUsuario(string stringConnection, long IDuser)
        {
            using var db = new UNG_Context();

            try
            {
                var first = (from usuario in db.Users
                             where usuario.ID == IDuser
                             && usuario.IDstatus == EncrypterService.Codify(StatusEnum.Active)
                             select new User_Request
                             {
                                 ID = usuario.ID,

                                 Name = EncrypterService.Decodify(usuario.Name),
                                 Surname = EncrypterService.Decodify(usuario.Surname),
                                 CompleteName = EncrypterService.Decodify(usuario.Name) + " " + EncrypterService.Decodify(usuario.Surname),
                                 Email = EncrypterService.Decodify(usuario.Email),

                                 //PermisosDeUsuario = (from permiso in db.PermisosDeUsers

                                 //                     where permiso.IDuser == usuario.ID

                                 //                     select new PermisosDeUsuario_Request
                                 //                     {
                                 //                         ID = permiso.ID,
                                 //                         IDuser = permiso.IDuser,

                                 //                         IDunidadDeNegocio = permiso.IDunidadDeNegocio,

                                 //                         IDseccion = permiso.IDseccion,
                                 //                         Seccion = permiso.Seccion,

                                 //                         Crear = permiso.Crear,
                                 //                         Modificar = permiso.Modificar,
                                 //                         Eliminar = permiso.Eliminar,
                                 //                         Ver = permiso.Ver,
                                 //                         Exportar = permiso.Exportar,
                                 //                     }).ToList()
                             }).FirstOrDefault();

                return first;
            }
            catch (Exception)
            {
                throw new Exception("No se pudo buscar el usuario logueado.");
            }
        }

        public static User_Request BuscarUsuario(ClaimsPrincipal user)
        {

            using var db = new UNG_Context();

            try
            {
                var IDuser = UsersClass.GetID_User(user);

                var first = (from usuario in db.Users
                             where usuario.ID == IDuser
                             && usuario.IDstatus == EncrypterService.Codify(StatusEnum.Active)
                             select new User_Request
                             {
                                 ID = usuario.ID,
                                 Name = EncrypterService.Decodify(usuario.Name),
                                 Surname = EncrypterService.Decodify(usuario.Surname),
                                 CompleteName = EncrypterService.Decodify(usuario.Name) + " " + EncrypterService.Decodify(usuario.Surname),
                                 Email = EncrypterService.Decodify(usuario.Email),

                                 //PermisosDeUsuario = (from permiso in db.PermisosDeUsers

                                 //                     where permiso.IDuser == usuario.ID

                                 //                     select new PermisosDeUsuario_Request
                                 //                     {
                                 //                         ID = permiso.ID,
                                 //                         IDuser = permiso.IDuser,

                                 //                         IDunidadDeNegocio = permiso.IDunidadDeNegocio,

                                 //                         IDseccion = permiso.IDseccion,
                                 //                         Seccion = permiso.Seccion,

                                 //                         Crear = permiso.Crear,
                                 //                         Modificar = permiso.Modificar,
                                 //                         Eliminar = permiso.Eliminar,
                                 //                         Ver = permiso.Ver,
                                 //                         Exportar = permiso.Exportar,
                                 //                     }).ToList()
                             }).FirstOrDefault();

                return first;
            }
            catch (Exception)
            {
                throw new Exception("No se pudo buscar el usuario logueado.");
            }
        }


        public static List<User_Request> ListaDeUsers(ClaimsPrincipal user)
        {
            using var db = new UNG_Context();

            try
            {
                var lista = (from usuario in db.Users
                             where usuario.IDstatus == EncrypterService.Codify(StatusEnum.Active)
                             select new User_Request
                             {
                                 ID = usuario.ID,
                                 Name = EncrypterService.Decodify(usuario.Name),
                                 Surname = EncrypterService.Decodify(usuario.Surname),
                                 CompleteName = EncrypterService.Decodify(usuario.Name) + " " + EncrypterService.Decodify(usuario.Surname),
                                 Email = EncrypterService.Decodify(usuario.Email),

                                 //PermisosDeUsuario = (from permiso in db.PermisosDeUsers

                                 //                     where permiso.IDuser == usuario.ID

                                 //                     select new PermisosDeUsuario_Request
                                 //                     {
                                 //                         ID = permiso.ID,
                                 //                         IDuser = permiso.IDuser,

                                 //                         IDunidadDeNegocio = permiso.IDunidadDeNegocio,

                                 //                         IDseccion = permiso.IDseccion,
                                 //                         Seccion = permiso.Seccion,

                                 //                         Crear = permiso.Crear,
                                 //                         Modificar = permiso.Modificar,
                                 //                         Eliminar = permiso.Eliminar,
                                 //                         Ver = permiso.Ver,
                                 //                         Exportar = permiso.Exportar,
                                 //                     }).ToList()
                             }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(user,
                        new New_Error_Request()
                        {
                            Comentario = "No se pudo buscar la lista de Users",
                            Excepcion = e,
                            Accion = AccionesDelSistemaEnum.BuscarLista,
                            Sistema = TiposDeSistemaEnum.API,
                        });

                throw new Exception("No se pudo buscar la lista de Users.");
            }
        }

        //public static List<Usuario_Request> ListaDeUsersConRolEnEspecial(ClaimsPrincipal user, string _seccion)
        //{
        //    using var db = new UNG_Context();

        //    try
        //    {
        //        var lista = (from permiso in db.PermisosDeUsers

        //                     join usuario in db.Users
        //                     on permiso.IDuser equals usuario.ID
        //                     into caq
        //                     from usuario in caq.DefaultIfEmpty()

        //                     where usuario.IDstatus == EncrypterService.Codify(EstadosEnum.Activo)
        //                     && permiso.Seccion.ToLower() == _seccion.ToLower()
        //                     && (permiso.Crear || permiso.Ver || permiso.Modificar || permiso.Eliminar || permiso.Exportar)

        //                     select new Usuario_Request
        //                     {
        //                         ID = usuario.ID,
        //                         Name = EncrypterService.Decodify(usuario.Name),
        //                         Surname = EncrypterService.Decodify(usuario.Surname),
        //                         NameCompleto = EncrypterService.Decodify(usuario.Name) + " " + EncrypterService.Decodify(usuario.Surname),
        //                         Email = EncrypterService.Decodify(usuario.Email),

        //                         //IDstatus = EncrypterService.Decodify(usuario.IDstatus),
        //                         //Area = EncrypterService.Decodify(usuario.Area),
        //                         Cargo = EncrypterService.Decodify(usuario.Cargo),
        //                         //Celular = EncrypterService.Decodify(usuario.Celular),
        //                         Telefono = EncrypterService.Decodify(usuario.Telefono),
        //                         //Direccion = EncrypterService.Decodify(usuario.Direccion),
        //                         //Ciudad = EncrypterService.Decodify(usuario.Ciudad),
        //                         //Provincia = EncrypterService.Decodify(usuario.Provincia),
        //                         //Pais = EncrypterService.Decodify(usuario.Pais),
        //                         //DNI = EncrypterService.Decodify(usuario.DNI),
        //                         //CUIL = EncrypterService.Decodify(usuario.CUIL),
        //                         Identificador = EncrypterService.Decodify(usuario.Identificador),
        //                         //URL_ImagenDePerfil = EncrypterService.Decodify(usuario.URL_ImagenDePerfil),
        //                         //Aux = EncrypterService.Decodify(usuario.Aux),
        //                         //Comentario = EncrypterService.Decodify(usuario.Comentario),

        //                         FechaDeNacimiento = EncrypterService.DecodifyDateTime(usuario.FechaDeNacimiento),
        //                         //FechaAltaDeUsuario = DateTime.Parse(EncrypterService.Decodify(usuario.FechaAltaDeUsuario)),
        //                         //FechaUltimoIngreso = DateTime.Parse(EncrypterService.Decodify(usuario.FechaUltimoIngreso)),

        //                         PermisosDeUsuario = (from permiso in db.PermisosDeUsers

        //                                              where permiso.IDuser == usuario.ID

        //                                              select new PermisosDeUsuario_Request
        //                                              {
        //                                                  ID = permiso.ID,
        //                                                  IDuser = permiso.IDuser,

        //                                                  IDunidadDeNegocio = permiso.IDunidadDeNegocio,

        //                                                  IDseccion = permiso.IDseccion,
        //                                                  Seccion = permiso.Seccion,

        //                                                  Crear = permiso.Crear,
        //                                                  Modificar = permiso.Modificar,
        //                                                  Eliminar = permiso.Eliminar,
        //                                                  Ver = permiso.Ver,
        //                                                  Exportar = permiso.Exportar,
        //                                              }).ToList()
        //                     }).ToList();

        //        return lista.GroupBy(p => p.ID).Select(g => g.First()).ToList();
        //    }
        //    catch (Exception e)
        //    {
        //        Logs_ErroresClass.NuevoLog(user,
        //                new New_Error_Request()
        //                {
        //                    Comentario = "No se pudo buscar la lista de Users con algún rol especial",
        //                    Excepcion = e,
        //                    Accion = AccionesDelSistemaEnum.BuscarLista,
        //                    Sistema = TiposDeSistemaEnum.API,
        //                });

        //        throw new Exception("No se pudo buscar la lista de Users con algún rol especial.");
        //    }
        //}

        public static Users LoginDeUsuario(UNG_Context db, string email, string password)
        {
            try
            {
                string passwordEncrypted = EncrypterService.GetSHA256(password);
                string emailEncrypted = EncrypterService.Codify(email.ToLower().Trim());
                string estadoEncrypted = EncrypterService.Codify(StatusEnum.Active);

                return db.Users.Where(x => x.Email == emailEncrypted && x.PasswordHash == passwordEncrypted && x.IDstatus == estadoEncrypted).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion Get


        #region Post
        internal static Respuesta CambiarContraseña(ClaimsPrincipal user, CambiarContraseñaUsuario_Request model)
        {
            using var db = new UNG_Context();
            using var transactionAdmin = db.Database.BeginTransaction();

            try
            {

                var iduser = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var usuarioAdmin = db.Users.Where(x => x.ID.ToString() == iduser).FirstOrDefault();
                if (usuarioAdmin == null) throw new Exception("No se encontró el usuario");

                if (usuarioAdmin.PasswordHash != EncrypterService.GetSHA256(model.ContraseñaAnterior))
                    throw new Exception("La contraseña anterior es incorrecta");

                if (model.NuevaContraseña != model.RepetirNuevaContraseña)
                    throw new Exception("Las contraseñas no coinciden");


                usuarioAdmin.PasswordHash = EncrypterService.GetSHA256(model.NuevaContraseña);
                db.Entry(usuarioAdmin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();

                transactionAdmin.Commit();

                Task.Run(async () =>
                {
                    await Logs_MovimientosDelSistemaClass.CambiarContraseña_Usuario(user, usuarioAdmin);
                });

                return new Respuesta(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                transactionAdmin.Rollback();

                Logs_ErroresClass.NuevoLog(user, new New_Error_Request()
                {
                    Comentario = "No se pudo cambiar la contraseña del usuario",
                    Excepcion = e,
                    Accion = AccionesDelSistemaEnum.Crear,
                    Sistema = TiposDeSistemaEnum.API,
                });

                return new Respuesta(StatusCodes.Status500InternalServerError, "No se pudo cambiar la contraseña. " + e.Message.ToString());
            }
        }

        public static Respuesta Crear(ClaimsPrincipal user, User_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                if (ExisteEmailEnBD(db, model.Email)) throw new Exception("Ya existe el email en la base de datos");

                var password = Guid.NewGuid().ToString().Remove(5);
                model.Contraseña = password;

                var usuario = new Users
                {
                    Email = EncrypterService.Codify(model.Email.ToLower().Trim()),
                    PasswordHash = EncrypterService.GetSHA256(model.Contraseña),
                    IDstatus = EncrypterService.Codify(StatusEnum.Active),
                    IDcompany = GetID_Company(user),
                    Name = EncrypterService.Codify(model.Name),
                    Surname = EncrypterService.Codify(model.Surname),
                    DateOf_Creation = EncrypterService.Codify(DateTime.Now.ToString()),
                };

                db.Users.Add(usuario);
                db.SaveChanges();

                //#region Permisos
                //foreach (var item in model.PermisosDeUsuario)
                //{
                //    var permiso = new PermisosDeUsers()
                //    {
                //        IDuser = usuario.ID,
                //        IDseccion = item.IDseccion,
                //        Seccion = item.Seccion,
                //        IDunidadDeNegocio = item.IDunidadDeNegocio,
                //        Ver = item.Ver,
                //        Crear = item.Crear,
                //        Modificar = item.Modificar,
                //        Eliminar = item.Eliminar,
                //        Exportar = item.Exportar,
                //    };
                //    db.PermisosDeUsers.Add(permiso);
                //}
                //db.SaveChanges();
                //#endregion Permisos


                transaction.Commit();

                Task.Run(async () =>
                {
                    await EmailClass.EnviarContraseñaParaNuevoUsuario(user, model, model.Contraseña);
                    await Logs_MovimientosDelSistemaClass.Crear_Usuario(user, usuario);
                });

                return new Respuesta(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                transaction.Rollback();

                Logs_ErroresClass.NuevoLog(user, new New_Error_Request()
                {
                    Comentario = "No se pudo crear el usuario",
                    Excepcion = e,
                    Accion = AccionesDelSistemaEnum.Crear,
                    Sistema = TiposDeSistemaEnum.API,
                });

                return new Respuesta(StatusCodes.Status500InternalServerError, "No se pudo crear. " + e.Message.ToString());
            }
        }

        public static Respuesta Modificar(ClaimsPrincipal user, User_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                var usuario = db.Users.Find(model.ID);
                if (usuario == null)
                    throw new Exception("No se encontró el usuario");

                usuario.Name = EncrypterService.Codify(model.Name);
                usuario.Surname = EncrypterService.Codify(model.Surname);
                usuario.Email = EncrypterService.Codify(model.Email.ToLower().Trim());

                db.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();


                //#region Permisos
                //var permisos = db.PermisosDeUsers.Where(x => x.IDuser == model.ID).ToList();
                //db.PermisosDeUsers.RemoveRange(permisos);
                //db.SaveChanges();

                //foreach (var item in model.PermisosDeUsuario)
                //{
                //    var permiso = new PermisosDeUsers()
                //    {
                //        IDuser = usuario.ID,
                //        IDseccion = item.IDseccion,
                //        Seccion = item.Seccion,
                //        IDunidadDeNegocio = item.IDunidadDeNegocio,
                //        Ver = item.Ver,
                //        Crear = item.Crear,
                //        Modificar = item.Modificar,
                //        Eliminar = item.Eliminar,
                //        Exportar = item.Exportar,
                //    };
                //    db.PermisosDeUsers.Add(permiso);
                //}
                //db.SaveChanges();
                //#endregion Permisos


                transaction.Commit();


                Task.Run(async () =>
                {
                    await Logs_MovimientosDelSistemaClass.Modificar_Usuario(user, usuario);
                });

                return new Respuesta(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                transaction.Rollback();

                Logs_ErroresClass.NuevoLog(user, new New_Error_Request()
                {
                    Comentario = "No se pudo modificar el usuario",
                    Excepcion = e,
                    Accion = AccionesDelSistemaEnum.Modificar,
                    Sistema = TiposDeSistemaEnum.API,
                });

                return new Respuesta(StatusCodes.Status500InternalServerError, "No se pudo modificar. " + e.Message.ToString());
            }
        }

        public static Respuesta Eliminar(ClaimsPrincipal user, EliminarUsuario_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                if (GetID_User(user) == model.ID)
                    throw new Exception("No puede eliminar su propio usuario");

                var usuario = db.Users.Find(model.ID);
                if (usuario == null)
                    throw new Exception("No se encontró el usuario");

                usuario.IDstatus = EncrypterService.Codify(StatusEnum.Deleted);
                db.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();


                //var permisos = db.PermisosDeUsers.Where(x => x.IDuser == model.ID).ToList();
                //db.PermisosDeUsers.RemoveRange(permisos);
                //db.SaveChanges();


                transaction.Commit();


                #region Guardado de movimientos
                Task.Run(async () =>
                {
                    await Logs_MovimientosDelSistemaClass.Eliminar_Usuario(user, usuario);
                });
                #endregion Guardado de movimientos


                return new Respuesta(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                transaction.Rollback();

                Logs_ErroresClass.NuevoLog(user, new New_Error_Request()
                {
                    Comentario = "No se pudo eliminar el usuario",
                    Excepcion = e,
                    Accion = AccionesDelSistemaEnum.Eliminar,
                    Sistema = TiposDeSistemaEnum.API,
                });

                return new Respuesta(StatusCodes.Status500InternalServerError, "No se pudo eliminar. " + e.Message.ToString());
            }
        }

        public static void ActualizarFechaUltimoIngreso(ClaimsPrincipal user, string Email)
        {
            using var db = new UNG_Context();

            try
            {
                var usuario = db.Users.Where(x => x.Email.ToLower() == EncrypterService.Codify(Email.ToLower().Trim())).FirstOrDefault();
                if (usuario == null) return;
                usuario.DateOf_LastLogin = EncrypterService.Codify(DateTime.Now.ToString());
                db.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(user, new New_Error_Request()
                {
                    Comentario = "No se pudo acutalizar la fecha del ultimo ingreso",
                    Excepcion = e,
                    Accion = AccionesDelSistemaEnum.Eliminar,
                    Sistema = TiposDeSistemaEnum.API,
                });
            }
        }
        #endregion Post


        #region Metodos
        private static bool ExisteEmailEnBD(UNG_Context db, string email)
        {
            try
            {
                var user = db.Users.Where(x => x.Email == EncrypterService.Codify(email)
                && x.IDstatus == EncrypterService.Codify(StatusEnum.Active)).FirstOrDefault();

                if (user == null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public static bool UsuarioTienePermiso(ClaimsPrincipal user, string seccion, string accion)
        {
            var usuario = BuscarUsuario(user);
            return UsuarioTienePermiso(usuario, seccion, accion);
        }

        public static bool UsuarioTienePermiso(User_Request user, string seccion, string accion)
        {
            try
            {
                if (user == null)
                    return false;

                return accion switch
                {
                    "Ver" => (user.PermisosDeUsuario.Where(x => x.Seccion == seccion && x.Ver).FirstOrDefault() != null),
                    "Crear" => (user.PermisosDeUsuario.Where(x => x.Seccion == seccion && x.Crear).FirstOrDefault() != null),
                    "Modificar" => (user.PermisosDeUsuario.Where(x => x.Seccion == seccion && x.Modificar).FirstOrDefault() != null),
                    "Eliminar" => (user.PermisosDeUsuario.Where(x => x.Seccion == seccion && x.Eliminar).FirstOrDefault() != null),
                    "Exportar" => (user.PermisosDeUsuario.Where(x => x.Seccion == seccion && x.Exportar).FirstOrDefault() != null),
                    _ => false,
                };
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<long> GetIDs_UnidadesDeNegocio(ClaimsPrincipal user)
        {
            var lista = new List<long>();
            try
            {
                using var db = new UNG_Context();

                lista = db.Companies.Where(x => x.IDstatus == StatusEnum.Active)
                        .Select(x => x.ID)
                        .ToList();
            }
            catch (Exception)
            { }

            return lista;
        }

        public static long GetID_Company(ClaimsPrincipal user)
        {
            try
            {
                return long.Parse(user.FindFirst(ClaimTypes.PrimarySid)?.Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static long GetID_User(ClaimsPrincipal user)
        {
            try
            {
                return long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion Metodos

    }
}
