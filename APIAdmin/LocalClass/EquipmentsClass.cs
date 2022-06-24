using APIAdmin.LocalModels.EntityFrameworks;
using Class;
using Models.Enums;
using Models.Global;
using Models.Request;
using Newtonsoft.Json;
using System.Security.Claims;

namespace APIAdmin.LocalClass
{
    public static class EquipmentsClass
    {
        #region Get
        public static GlobalResponse_Request CompleteInformation(ClaimsPrincipal _user, long IDstation)
        {
            var data = new GlobalResponse_Request(UsersClass.JSONListOfPermissions(_user))
            {
                ListOfEquipments = new()
            };

            data.ListOfEquipments = ListOfEquipments(_user, IDstation);

            if (data.ListOfEquipments != null && data.ListOfEquipments.Count != 0)
            {
                //    var nfi = new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." };

                //    data.Cantidad = data.ListaDeIngresos.Count.ToString();

                //    decimal Cobrado = data.ListaDeIngresos.Where(x => x.IDestado_Ingreso == EstadosDeIngresosEnum.Cobrado).Sum(x => x.Monto);
                //    data.Cobrado_Pagado = Cobrado.ToString("#,##0.00", nfi);

                //    decimal ACobrar = data.ListaDeIngresos.Where(x => x.IDestado_Ingreso == EstadosDeIngresosEnum.ACobrar).Sum(x => x.Monto);
                //    data.ACobrar_APagar = ACobrar.ToString("#,##0.00", nfi);

                //    decimal total = Cobrado + ACobrar;
                //    data.Total = total.ToString("#,##0.00", nfi);
            }


            return data;
        }
        public static GlobalResponse_Request PartialInformation(ClaimsPrincipal _user, long IDstation)
        {

            var data = new GlobalResponse_Request(UsersClass.JSONListOfPermissions(_user))
            {
                ListOfEquipments = new()
            };

            data.ListOfEquipments = ListOfEquipments(_user, IDstation);

            return data;
        }

        private static List<Equipment_Request> ListOfEquipments(ClaimsPrincipal _user, long IDstation, Filter_Request filter = null)
        {
            using var db = new UNG_Context();

            try
            {
                var lista = (from Equipment in db.Equipments

                             where Equipment.IDstatus != EquipmentStatusEnum.Deleted
                             && Equipment.IDstation == IDstation

                             select new Equipment_Request
                             {
                                 ID = Equipment.ID,
                                 IDstatus = Equipment.IDstatus,
                                 Name = Equipment.Name,

                                 Modify_Date = Equipment.Modify_Date,
                                 Modify_IDuser = Equipment.Modify_IDuser,

                                 IDstation = Equipment.IDstation,
                                 MAC = Equipment.MAC,

                                 Type = Equipment.Type,

                                 Aux = Equipment.Aux,
                             }).OrderByDescending(x => x.Name).ToList();

                #region Filtro

                //if (filterModel.ID != null && filterModel.IDestado_Ingreso.Count != 0)
                //{
                //    try
                //    {
                //        var temp = lista.Where(y => filterModel.IDestado_Ingreso.Any(z => z == y.IDestado_Ingreso)).ToList();
                //        lista = temp;
                //    }
                //    catch (Exception)
                //    { }
                //}
                #endregion Filtro

                return lista;
            }
            catch (Exception e)
            {
                Logs_ErrorsClass.NuevoLog(_user, "Could not load list of equipments", SystemActionsEnum.SearchList, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                throw new Exception("Could not load list of equipments.");
            }
        }

        private static Equipment_Request SearchEquipment(ClaimsPrincipal _user, long IDequipment)
        {
            using var db = new UNG_Context();

            try
            {
                var lista = (from Equipment in db.Equipments

                             where Equipment.IDstatus == EquipmentStatusEnum.Enabled
                             && Equipment.ID == IDequipment

                             select new Equipment_Request
                             {
                                 ID = Equipment.ID,
                                 IDstatus = Equipment.IDstatus,
                                 Name = Equipment.Name,

                                 Modify_Date = Equipment.Modify_Date,
                                 Modify_IDuser = Equipment.Modify_IDuser,

                                 IDstation = Equipment.IDstation,
                                 MAC = Equipment.MAC,
                                 Type = Equipment.Type,

                                 Aux = Equipment.Aux,
                             }).FirstOrDefault();

                return lista;
            }
            catch (Exception e)
            {
                Logs_ErrorsClass.NuevoLog(_user, "Could not search equipment", SystemActionsEnum.SearchList, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                throw new Exception("Could not search equipment.");
            }
        }
        #endregion Get


        public static GlobalResponse Create(ClaimsPrincipal _user, Equipment_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                var Equipment = new Equipments
                {
                    Modify_Date = DateTime.Now,
                    Modify_IDuser = GlobalClass.GetID_User(_user),
                    Name = model.Name,
                    IDstatus = model.IDstatus,
                    IDstation = model.IDstation,
                    Type = model.Type,
                    MAC = model.MAC,
                    Aux = model.Aux,
                };


                db.Equipments.Add(Equipment);
                db.SaveChanges();

                transaction.Commit();


                #region Save move
                Task.Run(async () =>
                {
                    await Logs_SystemMovesClass.Create_Equipment(_user, Equipment);
                });
                #endregion Guardado de movimientos

                return new GlobalResponse(StatusCodes.Status201Created, JsonConvert.SerializeObject(Equipment));
            }
            catch (Exception e)
            {
                transaction.Rollback();

                Logs_ErrorsClass.NuevoLog(_user, "Could not create the equipment", SystemActionsEnum.Create, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not create. " + e.Message.ToString());
            }
        }

        public static GlobalResponse Modify(ClaimsPrincipal _user, Equipment_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                var Equipment = db.Equipments.Find(model.ID);
                if (Equipment == null)
                    throw new Exception("Not found equipment");

                #region Modificar
                Equipment.Modify_Date = DateTime.Now;
                Equipment.Modify_IDuser = GlobalClass.GetID_User(_user);
                Equipment.Name = model.Name;
                Equipment.IDstatus = model.IDstatus;
                Equipment.IDstation = model.IDstation;
                Equipment.Type = model.Type;
                Equipment.MAC = model.MAC;
                Equipment.Aux = model.Aux;
                db.Entry(Equipment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                #endregion Modificar


                transaction.Commit();

                #region Guardado de movimientos
                Task.Run(async () =>
                {
                    await Logs_SystemMovesClass.Modify_Equipment(_user, Equipment);
                });
                #endregion Guardado de movimientos

                return new GlobalResponse(StatusCodes.Status201Created, JsonConvert.SerializeObject(Equipment));
            }
            catch (Exception e)
            {
                transaction.Rollback();

                Logs_ErrorsClass.NuevoLog(_user, "Could not modify equipment", SystemActionsEnum.Modify, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not modify. " + e.Message.ToString());
            }
        }

        public static GlobalResponse Delete(ClaimsPrincipal _user, DeleteEquipment_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                var Equipment = db.Equipments.Find(model.ID);
                if (Equipment == null)
                    throw new Exception("Not found Equipment");


                #region Modificacion del Equipment
                Equipment.Modify_Date = DateTime.Now;
                Equipment.Modify_IDuser = GlobalClass.GetID_User(_user);
                Equipment.IDstatus = EquipmentStatusEnum.Deleted;
                db.Entry(Equipment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                #endregion Modificacion del Equipment

                transaction.Commit();

                #region Guardado de movimientos
                Task.Run(async () =>
                {
                    await Logs_SystemMovesClass.Delete_Equipment(_user, Equipment);
                });
                #endregion Guardado de movimientos

                return new GlobalResponse(StatusCodes.Status200OK);

            }
            catch (Exception e)
            {
                transaction.Rollback();

                Logs_ErrorsClass.NuevoLog(_user, "Could not delete equipment", SystemActionsEnum.Delete, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not delete. " + e.Message.ToString());
            }
        }

    }
}
