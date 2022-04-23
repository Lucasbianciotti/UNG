using API.Services;
using Models.EntityFrameworks;
using Models.Enums;
using Models.Global;
using Models.Request;
using System.Security.Claims;

namespace API.LocalClass
{
    public static class UsuariosClass
    {

        #region Get
        public static GlobalResponse_Request InformacionCompleta(ClaimsPrincipal user)
        {

            var data = new GlobalResponse_Request()
            {
                Usuarios = ListaDeUsuarios(user),
            };

            return data;
        }

        public static GlobalResponse_Request InformacionParcial(ClaimsPrincipal user)
        {

            var data = new GlobalResponse_Request()
            {
                Usuarios = ListaDeUsuarios(user),
            };

            return data;
        }


        public static Usuario_Request BuscarUsuario(string stringConnection, long IDusuario)
        {
            using var db = new UNG_Context();

            try
            {
                var first = (from usuario in db.Usuarios
                             where usuario.ID == IDusuario
                             && usuario.IDestado == EncrypterService.Codify(EstadosEnum.Activo)
                             select new Usuario_Request
                             {
                                 ID = usuario.ID,

                                 Nombre = EncrypterService.Decodify(usuario.Nombre),
                                 Apellido = EncrypterService.Decodify(usuario.Apellido),
                                 NombreCompleto = EncrypterService.Decodify(usuario.Nombre) + " " + EncrypterService.Decodify(usuario.Apellido),
                                 Email = EncrypterService.Decodify(usuario.Email),

                                 //PermisosDeUsuario = (from permiso in db.PermisosDeUsuarios

                                 //                     where permiso.IDusuario == usuario.ID

                                 //                     select new PermisosDeUsuario_Request
                                 //                     {
                                 //                         ID = permiso.ID,
                                 //                         IDusuario = permiso.IDusuario,

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

        public static Usuario_Request BuscarUsuario(ClaimsPrincipal user)
        {

            using var db = new UNG_Context();

            try
            {
                var idUsuario = UsuariosClass.GetID_User(user);

                var first = (from usuario in db.Usuarios
                             where usuario.ID == idUsuario
                             && usuario.IDestado == EncrypterService.Codify(EstadosEnum.Activo)
                             select new Usuario_Request
                             {
                                 ID = usuario.ID,
                                 Nombre = EncrypterService.Decodify(usuario.Nombre),
                                 Apellido = EncrypterService.Decodify(usuario.Apellido),
                                 NombreCompleto = EncrypterService.Decodify(usuario.Nombre) + " " + EncrypterService.Decodify(usuario.Apellido),
                                 Email = EncrypterService.Decodify(usuario.Email),

                                 //PermisosDeUsuario = (from permiso in db.PermisosDeUsuarios

                                 //                     where permiso.IDusuario == usuario.ID

                                 //                     select new PermisosDeUsuario_Request
                                 //                     {
                                 //                         ID = permiso.ID,
                                 //                         IDusuario = permiso.IDusuario,

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


        public static List<Usuario_Request> ListaDeUsuarios(ClaimsPrincipal user)
        {
            using var db = new UNG_Context();

            try
            {
                var lista = (from usuario in db.Usuarios
                             where usuario.IDestado == EncrypterService.Codify(EstadosEnum.Activo)
                             select new Usuario_Request
                             {
                                 ID = usuario.ID,
                                 Nombre = EncrypterService.Decodify(usuario.Nombre),
                                 Apellido = EncrypterService.Decodify(usuario.Apellido),
                                 NombreCompleto = EncrypterService.Decodify(usuario.Nombre) + " " + EncrypterService.Decodify(usuario.Apellido),
                                 Email = EncrypterService.Decodify(usuario.Email),

                                 //PermisosDeUsuario = (from permiso in db.PermisosDeUsuarios

                                 //                     where permiso.IDusuario == usuario.ID

                                 //                     select new PermisosDeUsuario_Request
                                 //                     {
                                 //                         ID = permiso.ID,
                                 //                         IDusuario = permiso.IDusuario,

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
                            Comentario = "No se pudo buscar la lista de usuarios",
                            Excepcion = e,
                            Accion = AccionesDelSistemaEnum.BuscarLista,
                            Sistema = TiposDeSistemaEnum.API,
                        });

                throw new Exception("No se pudo buscar la lista de usuarios.");
            }
        }

        //public static List<Usuario_Request> ListaDeUsuariosConRolEnEspecial(ClaimsPrincipal user, string _seccion)
        //{
        //    using var db = new UNG_Context();

        //    try
        //    {
        //        var lista = (from permiso in db.PermisosDeUsuarios

        //                     join usuario in db.Usuarios
        //                     on permiso.IDusuario equals usuario.ID
        //                     into caq
        //                     from usuario in caq.DefaultIfEmpty()

        //                     where usuario.IDestado == EncrypterService.Codify(EstadosEnum.Activo)
        //                     && permiso.Seccion.ToLower() == _seccion.ToLower()
        //                     && (permiso.Crear || permiso.Ver || permiso.Modificar || permiso.Eliminar || permiso.Exportar)

        //                     select new Usuario_Request
        //                     {
        //                         ID = usuario.ID,
        //                         Nombre = EncrypterService.Decodify(usuario.Nombre),
        //                         Apellido = EncrypterService.Decodify(usuario.Apellido),
        //                         NombreCompleto = EncrypterService.Decodify(usuario.Nombre) + " " + EncrypterService.Decodify(usuario.Apellido),
        //                         Email = EncrypterService.Decodify(usuario.Email),

        //                         //IDestado = EncrypterService.Decodify(usuario.IDestado),
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

        //                         PermisosDeUsuario = (from permiso in db.PermisosDeUsuarios

        //                                              where permiso.IDusuario == usuario.ID

        //                                              select new PermisosDeUsuario_Request
        //                                              {
        //                                                  ID = permiso.ID,
        //                                                  IDusuario = permiso.IDusuario,

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
        //                    Comentario = "No se pudo buscar la lista de usuarios con algún rol especial",
        //                    Excepcion = e,
        //                    Accion = AccionesDelSistemaEnum.BuscarLista,
        //                    Sistema = TiposDeSistemaEnum.API,
        //                });

        //        throw new Exception("No se pudo buscar la lista de usuarios con algún rol especial.");
        //    }
        //}

        public static Usuarios LoginDeUsuario(UNG_Context db, string email, string password)
        {
            try
            {
                string passwordEncrypted = EncrypterService.GetSHA256(password);
                string emailEncrypted = EncrypterService.Codify(email.ToLower().Trim());
                string estadoEncrypted = EncrypterService.Codify(EstadosEnum.Activo);

                return db.Usuarios.Where(x => x.Email == emailEncrypted && x.PasswordHash == passwordEncrypted && x.IDestado == estadoEncrypted).FirstOrDefault();
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
                var usuarioAdmin = db.Usuarios.Where(x => x.ID.ToString() == iduser).FirstOrDefault();
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

        public static Respuesta Crear(ClaimsPrincipal user, Usuario_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                if (ExisteEmailEnBD(db, model.Email)) throw new Exception("Ya existe el email en la base de datos");

                var password = Guid.NewGuid().ToString().Remove(5);
                model.Contraseña = password;

                var usuario = new Usuarios
                {
                    Email = EncrypterService.Codify(model.Email.ToLower().Trim()),
                    PasswordHash = EncrypterService.GetSHA256(model.Contraseña),
                    IDestado = EncrypterService.Codify(EstadosEnum.Activo),
                    IDempresa = GetID_Company(user),
                    Nombre = EncrypterService.Codify(model.Nombre),
                    Apellido = EncrypterService.Codify(model.Apellido),
                    FechaAltaDeUsuario = EncrypterService.Codify(DateTime.Now.ToString()),
                };

                db.Usuarios.Add(usuario);
                db.SaveChanges();

                //#region Permisos
                //foreach (var item in model.PermisosDeUsuario)
                //{
                //    var permiso = new PermisosDeUsuarios()
                //    {
                //        IDusuario = usuario.ID,
                //        IDseccion = item.IDseccion,
                //        Seccion = item.Seccion,
                //        IDunidadDeNegocio = item.IDunidadDeNegocio,
                //        Ver = item.Ver,
                //        Crear = item.Crear,
                //        Modificar = item.Modificar,
                //        Eliminar = item.Eliminar,
                //        Exportar = item.Exportar,
                //    };
                //    db.PermisosDeUsuarios.Add(permiso);
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

        public static Respuesta Modificar(ClaimsPrincipal user, Usuario_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                var usuario = db.Usuarios.Find(model.ID);
                if (usuario == null)
                    throw new Exception("No se encontró el usuario");

                usuario.Nombre = EncrypterService.Codify(model.Nombre);
                usuario.Apellido = EncrypterService.Codify(model.Apellido);
                usuario.Email = EncrypterService.Codify(model.Email.ToLower().Trim());

                db.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();


                //#region Permisos
                //var permisos = db.PermisosDeUsuarios.Where(x => x.IDusuario == model.ID).ToList();
                //db.PermisosDeUsuarios.RemoveRange(permisos);
                //db.SaveChanges();

                //foreach (var item in model.PermisosDeUsuario)
                //{
                //    var permiso = new PermisosDeUsuarios()
                //    {
                //        IDusuario = usuario.ID,
                //        IDseccion = item.IDseccion,
                //        Seccion = item.Seccion,
                //        IDunidadDeNegocio = item.IDunidadDeNegocio,
                //        Ver = item.Ver,
                //        Crear = item.Crear,
                //        Modificar = item.Modificar,
                //        Eliminar = item.Eliminar,
                //        Exportar = item.Exportar,
                //    };
                //    db.PermisosDeUsuarios.Add(permiso);
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

                var usuario = db.Usuarios.Find(model.ID);
                if (usuario == null)
                    throw new Exception("No se encontró el usuario");

                usuario.IDestado = EncrypterService.Codify(EstadosEnum.Eliminado);
                db.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();


                //var permisos = db.PermisosDeUsuarios.Where(x => x.IDusuario == model.ID).ToList();
                //db.PermisosDeUsuarios.RemoveRange(permisos);
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
                var usuario = db.Usuarios.Where(x => x.Email.ToLower() == EncrypterService.Codify(Email.ToLower().Trim())).FirstOrDefault();
                if (usuario == null) return;
                usuario.FechaUltimoIngreso = EncrypterService.Codify(DateTime.Now.ToString());
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
                var user = db.Usuarios.Where(x => x.Email == EncrypterService.Codify(email)
                && x.IDestado == EncrypterService.Codify(EstadosEnum.Activo)).FirstOrDefault();

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

        public static bool UsuarioTienePermiso(Usuario_Request user, string seccion, string accion)
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

                lista = db.Empresas.Where(x => x.IDestado == EstadosEnum.Activo)
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
