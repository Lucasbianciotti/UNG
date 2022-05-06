using Models.EntityFrameworks;
using Models.Enums;
using Models.Global;
using Models.Request;
using System.Security.Claims;

namespace API.LocalClass
{
    public static class DataClass
    {
        #region Get
        public static GlobalResponse_Request CompleteInformation(ClaimsPrincipal _user, Filter_Request Filtros)
        {
            var data = new GlobalResponse_Request(UsersClass.JSONListOfPermissions(_user))
            {
                InformationOfData = new(),
            };

            data.InformationOfData.ListOfData = ListOfData(_user, Filtros);

            if (data.InformationOfData.ListOfData != null && data.InformationOfData.ListOfData.Count != 0)
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
        public static GlobalResponse_Request PartialInformation(ClaimsPrincipal _user, Filter_Request Filtros)
        {
            var data = new GlobalResponse_Request(UsersClass.JSONListOfPermissions(_user))
            {
                InformationOfData = new(),
            };

            data.InformationOfData.ListOfData = ListOfData(_user, Filtros);

            return data;
        }

        private static List<Data_Request> ListOfData(ClaimsPrincipal _user, Filter_Request filterModel)
        {
            using var db = new UNG_Context();

            try
            {
                var lista = (from Data in db.Data

                             select new Data_Request
                             {
                                 ID = Data.ID,
                                 Count = Data.Count,
                                 Timespan = Data.Timespan,
                                 IDequipment = Data.IDequipment,
                                 Equipment = (from equipment in db.Equipments
                                              where equipment.ID == Data.IDequipment
                                              select new Equipment_Request
                                              {
                                                  ID = equipment.ID,
                                                  IDstatus = equipment.IDstatus,
                                                  Name = equipment.Name,

                                                  Modify_Date = equipment.Modify_Date,
                                                  Modify_IDuser = equipment.Modify_IDuser,

                                                  IDstation = equipment.IDstation,
                                                  MAC = equipment.MAC,
                                              }).FirstOrDefault(),
                                 Info = Data.Info,
                                 Datetime = Data.Datetime,
                                 Aux = Data.Aux,
                             }).OrderByDescending(x => x.IDequipment).ToList();

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
                Logs_ErroresClass.NuevoLog(_user, "Could not load list of data", SystemActionsEnum.SearchList, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                throw new Exception("Could not load list of data.");
            }
        }
        #endregion Get


        public static GlobalResponse Create(ClaimsPrincipal _user, Data_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                var Data = new Data
                {
                    Timespan = model.Timespan,
                    IDequipment = model.IDequipment,
                    Datetime = model.Datetime,
                    Count = model.Count,
                    Info = model.Info,
                    Aux = model.Aux,
                };


                db.Data.Add(Data);
                db.SaveChanges();

                transaction.Commit();

                return new GlobalResponse(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                transaction.Rollback();

                Logs_ErroresClass.NuevoLog(_user, "Could not create the data", SystemActionsEnum.Create, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not create. " + e.Message.ToString());
            }
        }

        //public static GlobalResponse Modify(ClaimsPrincipal _user, Data_Request model)
        //{
        //    using var db = new UNG_Context();
        //    using var transaction = db.Database.BeginTransaction();

        //    try
        //    {
        //        var Data = db.Data.Find(model.ID);
        //        if (Data == null)
        //            throw new Exception("Not found data");

        //        #region Modificar

        //        Data.Aux = model.Aux;
        //        db.Entry(Data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        db.SaveChanges();
        //        #endregion Modificar


        //        transaction.Commit();

        //        #region Guardado de movimientos
        //        Task.Run(async () =>
        //        {
        //            await Logs_SystemMovesClass.Modify_Data(_user, Data);
        //        });
        //        #endregion Guardado de movimientos

        //        return new GlobalResponse(StatusCodes.Status201Created);
        //    }
        //    catch (Exception e)
        //    {
        //        transaction.Rollback();

        //        Logs_ErroresClass.NuevoLog(_user, "Could not modify data", SystemActionsEnum.Modify, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

        //        return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not modify. " + e.Message.ToString());
        //    }
        //}

        //public static GlobalResponse Delete(ClaimsPrincipal _user, DeleteData_Request model)
        //{
        //    using var db = new UNG_Context();
        //    using var transaction = db.Database.BeginTransaction();

        //    try
        //    {
        //        var Data = db.Data.Find(model.ID);
        //        if (Data == null)
        //            throw new Exception("Not found Data");


        //        #region Modificacion del Data
        //        Data.Modify_Date = DateTime.Now;
        //        Data.Modify_IDuser = UsersClass.GetID_User(_user);
        //        Data.IDstatus = DataStatusEnum.Deleted;
        //        db.Entry(Data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        db.SaveChanges();
        //        #endregion Modificacion del Data

        //        transaction.Commit();

        //        #region Guardado de movimientos
        //        Task.Run(async () =>
        //        {
        //            await Logs_SystemMovesClass.Delete_Data(_user, Data);
        //        });
        //        #endregion Guardado de movimientos

        //        return new GlobalResponse(StatusCodes.Status200OK);

        //    }
        //    catch (Exception e)
        //    {
        //        transaction.Rollback();

        //        Logs_ErroresClass.NuevoLog(_user, "Could not delete data", SystemActionsEnum.Delete, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

        //        return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not delete. " + e.Message.ToString());
        //    }
        //}

    }
}
