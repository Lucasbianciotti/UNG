using APIAdmin.LocalModels.EntityFrameworks;
using APIAdmin.Services;
using Class;
using Models.Enums;
using Models.Global;
using Models.Request;
using Newtonsoft.Json;
using System.Security.Claims;

namespace APIAdmin.LocalClass
{
    public static class UsersClass
    {

        #region Get
        public static GlobalResponse_Request CompleteInformation(ClaimsPrincipal _user, Filter_Request filtros)
        {

            var data = new GlobalResponse_Request(UsersClass.JSONListOfPermissions(_user))
            {
                ListOfUsers = ListOfUsers(_user),
                ListOfClients = ClientsClass.ListOfClients(_user, filtros),
            };

            return data;
        }

        public static GlobalResponse_Request PartialInformation(ClaimsPrincipal _user, Filter_Request filtros)
        {

            var data = new GlobalResponse_Request(UsersClass.JSONListOfPermissions(_user))
            {
                ListOfUsers = ListOfUsers(_user),
                ListOfClients = ClientsClass.ListOfClients(_user, filtros),
            };

            return data;
        }

        public static List<User_Request> ListOfUsers(ClaimsPrincipal _user)
        {
            using var db = new UNG_Context();

            try
            {
                var lista = (from user in db.Users
                             where user.IDstatus == EncodifierClass.Codify(StatusEnum.Active)
                             select new User_Request
                             {
                                 ID = user.ID,
                                 Name = user.Name,
                                 Surname = user.Surname,
                                 CompleteName = user.Name + " " + user.Surname,
                                 Email = user.Email,
                                 JSONListOfPermissions = user.JSONListOfPermissions,
                                 IDstatus = user.IDstatus,
                                 IDrole = user.IDrole,
                                 IDclient = user.IDclient,
                             }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                Logs_ErrorsClass.NuevoLog(_user,
                        new New_Error_Request()
                        {
                            Comentario = "Could not load list of users",
                            Excepcion = e,
                            Accion = SystemActionsEnum.SearchList,
                            Sistema = SystemTypesEnum.API,
                        });

                throw new Exception("Could not load list of users.");
            }
        }


        //public static User_Request SearchUser(ClaimsPrincipal _user)
        //{

        //    using var db = new UNG_Context();

        //    try
        //    {
        //        var IDuser = GlobalClass.GetID_User(_user);

        //        var user = db.Users.Where(x => x.ID == IDuser && x.IDstatus == EncrypterClass.Codify(StatusEnum.Active)).FirstOrDefault();

        //        var User= new User_Request()
        //                     {
        //                         ID = user.ID,
        //                         Name = EncrypterClass.Decodify(user.Name),
        //                         Surname = EncrypterClass.Decodify(user.Surname),
        //                         CompleteName = EncrypterClass.Decodify(user.Name) + " " + EncrypterClass.Decodify(user.Surname),
        //                         Email = EncrypterClass.Decodify(user.Email),
        //                         IDclient = user.IDclient,
        //                         IDrole = user.IDrole,
        //                         IDstatus = EncrypterClass.Decodify(user.IDstatus),

        //                         //PermissionsForSection = JsonConvert.DeserializeObject<List<Permissions_Request>>(user.JSONListOfPermissions),
        //                     };

        //        return first;
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Could not search user.");
        //    }
        //}

        #endregion Get


        #region Post
        internal static GlobalResponse ChangePassword(ClaimsPrincipal _user, ChangePasswordUser_Request model)
        {
            using var db = new UNG_Context();
            using var transactionAdmin = db.Database.BeginTransaction();

            try
            {

                var iduser = _user.FindFirst(ClaimTypes.PrimarySid)?.Value;

                var userAdmin = db.Users.Where(x => x.ID.ToString() == iduser).FirstOrDefault();
                if (userAdmin == null) throw new Exception("Not found user");

                if (userAdmin.PasswordHash != EncrypterService.GetSHA256(model.PasswordOld))
                    throw new Exception("Last password is incorrect");

                if (model.NewPassword != model.RepeatNewPassword)
                    throw new Exception("Passwords do not match");


                userAdmin.PasswordHash = EncrypterService.GetSHA256(model.NewPassword);
                db.Entry(userAdmin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();

                transactionAdmin.Commit();

                Task.Run(async () =>
                {
                    await Logs_SystemMovesClass.ChangePassword_User(_user, userAdmin);
                });

                return new GlobalResponse(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                transactionAdmin.Rollback();

                Logs_ErrorsClass.NuevoLog(_user, new New_Error_Request()
                {
                    Comentario = "Could not change password user",
                    Excepcion = e,
                    Accion = SystemActionsEnum.Create,
                    Sistema = SystemTypesEnum.API,
                });

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not change password user. " + e.Message.ToString());
            }
        }

        public static GlobalResponse Create(ClaimsPrincipal _user, User_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                if (ExisteEmailEnBD(db, model.Email)) throw new Exception("The email already exists.");

                var password = Guid.NewGuid().ToString().Remove(5);
                model.Password = password;

                var user = new Users
                {
                    Email = EncodifierClass.Codify(model.Email.ToLower().Trim()),
                    PasswordHash = EncrypterService.GetSHA256(model.Password),
                    IDstatus = EncodifierClass.Codify(StatusEnum.Active),
                    IDclient = GlobalClass.GetID_Client(_user),
                    Name = EncodifierClass.Codify(model.Name),
                    Surname = EncodifierClass.Codify(model.Surname),
                    DateOf_Creation = EncodifierClass.Codify(DateTime.Now.ToString()),
                };

                db.Users.Add(user);
                db.SaveChanges();

                //#region Permisos
                //foreach (var item in model.PermisosDeUser)
                //{
                //    var permiso = new PermisosDeUsers()
                //    {
                //        IDuser = user.ID,
                //        IDseccion = item.IDseccion,
                //        Seccion = item.Seccion,
                //        IDunidadDeNegocio = item.IDunidadDeNegocio,
                //        Read = item.Read,
                //        Create = item.Create,
                //        Modify = item.Modify,
                //        Delete = item.Delete,
                //        Export = item.Export,
                //    };
                //    db.PermisosDeUsers.Add(permiso);
                //}
                //db.SaveChanges();
                //#endregion Permisos


                transaction.Commit();

                Task.Run(async () =>
                {
                    await EmailClass.SendPasswordForNewUser(_user, model, model.Password);
                    await Logs_SystemMovesClass.Create_User(_user, user);
                });

                return new GlobalResponse(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                transaction.Rollback();

                Logs_ErrorsClass.NuevoLog(_user, new New_Error_Request()
                {
                    Comentario = "Could not create user",
                    Excepcion = e,
                    Accion = SystemActionsEnum.Create,
                    Sistema = SystemTypesEnum.API,
                });

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not create user. " + e.Message.ToString());
            }
        }

        public static GlobalResponse Modify(ClaimsPrincipal _user, User_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                var user = db.Users.Find(model.ID);
                if (user == null)
                    throw new Exception("Not found user");

                user.Name = EncodifierClass.Codify(model.Name);
                user.Surname = EncodifierClass.Codify(model.Surname);
                user.Email = EncodifierClass.Codify(model.Email.ToLower().Trim());

                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();


                //#region Permisos
                //var permisos = db.PermisosDeUsers.Where(x => x.IDuser == model.ID).ToList();
                //db.PermisosDeUsers.RemoveRange(permisos);
                //db.SaveChanges();

                //foreach (var item in model.PermisosDeUser)
                //{
                //    var permiso = new PermisosDeUsers()
                //    {
                //        IDuser = user.ID,
                //        IDseccion = item.IDseccion,
                //        Seccion = item.Seccion,
                //        IDunidadDeNegocio = item.IDunidadDeNegocio,
                //        Read = item.Read,
                //        Create = item.Create,
                //        Modify = item.Modify,
                //        Delete = item.Delete,
                //        Export = item.Export,
                //    };
                //    db.PermisosDeUsers.Add(permiso);
                //}
                //db.SaveChanges();
                //#endregion Permisos


                transaction.Commit();


                Task.Run(async () =>
                {
                    await Logs_SystemMovesClass.Modify_User(_user, user);
                });

                return new GlobalResponse(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                transaction.Rollback();

                Logs_ErrorsClass.NuevoLog(_user, new New_Error_Request()
                {
                    Comentario = "Could not modify user",
                    Excepcion = e,
                    Accion = SystemActionsEnum.Modify,
                    Sistema = SystemTypesEnum.API,
                });

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not modify user. " + e.Message.ToString());
            }
        }

        public static GlobalResponse Delete(ClaimsPrincipal _user, DeleteUser_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                if (GlobalClass.GetID_User(_user) == model.ID)
                    throw new Exception("No puede eliminar su propio user");

                var user = db.Users.Find(model.ID);
                if (user == null)
                    throw new Exception("Not found user");

                user.IDstatus = EncodifierClass.Codify(StatusEnum.Deleted);
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();


                //var permisos = db.PermisosDeUsers.Where(x => x.IDuser == model.ID).ToList();
                //db.PermisosDeUsers.RemoveRange(permisos);
                //db.SaveChanges();


                transaction.Commit();


                #region Guardado de movimientos
                Task.Run(async () =>
                {
                    await Logs_SystemMovesClass.Delete_User(_user, user);
                });
                #endregion Guardado de movimientos


                return new GlobalResponse(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                transaction.Rollback();

                Logs_ErrorsClass.NuevoLog(_user, new New_Error_Request()
                {
                    Comentario = "Could not delete user",
                    Excepcion = e,
                    Accion = SystemActionsEnum.Delete,
                    Sistema = SystemTypesEnum.API,
                });

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not delete user. " + e.Message.ToString());
            }
        }

        public static void ActualizarFechaUltimoIngreso(ClaimsPrincipal _user, string Email)
        {
            using var db = new UNG_Context();

            try
            {
                var user = db.Users.Where(x => x.Email.ToLower() == EncodifierClass.Codify(Email.ToLower().Trim())).FirstOrDefault();
                if (user == null) return;
                user.DateOf_LastLogin = EncodifierClass.Codify(DateTime.Now.ToString());
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Logs_ErrorsClass.NuevoLog(_user, new New_Error_Request()
                {
                    Comentario = "Could not change date",
                    Excepcion = e,
                    Accion = SystemActionsEnum.Delete,
                    Sistema = SystemTypesEnum.API,
                });
            }
        }
        #endregion Post


        #region Metodos
        public static string JSONListOfPermissions(ClaimsPrincipal _user)
        {

            using var db = new UNG_Context();

            try
            {
                var IDuser = GlobalClass.GetID_User(_user);

                var user = db.Users.Where(x => x.ID == IDuser && x.IDstatus == EncodifierClass.Codify(StatusEnum.Active)).FirstOrDefault();

                if (user != null) return user.JSONListOfPermissions;

                else return String.Empty;
            }
            catch (Exception)
            {
                throw new Exception("Could not search permissions user.");
            }
        }

        private static bool ExisteEmailEnBD(UNG_Context db, string email)
        {
            try
            {
                var user = db.Users.Where(x => x.Email == EncodifierClass.Codify(email)
                && x.IDstatus == EncodifierClass.Codify(StatusEnum.Active)).FirstOrDefault();

                if (user == null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public static bool UserHasPermission(ClaimsPrincipal _user, SystemSectionsEnum seccion, SystemActionsEnum accion)
        {
            try
            {
                var db = new UNG_Context();
                var IDuser = GlobalClass.GetID_User(_user);

                var user = db.Users.Where(x => x.ID == IDuser && x.IDstatus == EncodifierClass.Codify(StatusEnum.Active)).FirstOrDefault();
                if (user == null) return false;

                var permissions = JsonConvert.DeserializeObject<List<Permissions_Request>>(EncodifierClass.Decodify(user.JSONListOfPermissions));
                if (permissions == null) return false;

                var permission = permissions.Where(x => x.IDsection == (int)seccion).FirstOrDefault();
                if (permission == null) return false;


                if (accion == SystemActionsEnum.Read) return permission.Read;
                else if (accion == SystemActionsEnum.Create) return permission.Create;
                else if (accion == SystemActionsEnum.Modify) return permission.Modify;
                else if (accion == SystemActionsEnum.Delete) return permission.Delete;
                else if (accion == SystemActionsEnum.Export) return permission.Export;
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion Metodos

    }
}
