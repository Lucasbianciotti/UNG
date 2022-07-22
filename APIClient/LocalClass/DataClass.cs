using APIClient.LocalModels.SQLite;
using CommonModels.Enums;
using CommonModels.Global;
using CommonModels.Request;
using Newtonsoft.Json;
using System.Security.Claims;

namespace APIClient.LocalClass
{
    public static class DataClass
    {
        #region Get
        public static LocalResponse_Request CompleteInformation(ClaimsPrincipal _user, FilterData_Request Filtros, string URL)
        {
            var data = new LocalResponse_Request(ClientsClass.SearchClient(_user), StationsClass.SearchStation(_user, URL), UsersClass.SearchUser(_user))
            {
                ListOfData = ListOfData(_user, Filtros),
            };

            return data;
        }

        public static List<Data_Request> ListOfData(ClaimsPrincipal _user, FilterData_Request filterModel)
        {
            using var db = new Local_Context();

            try
            {
                filterModel.Date_Start = new DateTime(filterModel.Date_Start.Year, filterModel.Date_Start.Month, filterModel.Date_Start.Day, 0, 0, 0);
                filterModel.Date_End = new DateTime(filterModel.Date_End.Year, filterModel.Date_End.Month, filterModel.Date_End.Day, 23, 59, 59);

                var lista = (from data in db.Data

                             where (
                                (data.Datetime >= filterModel.Date_Start && data.Datetime < filterModel.Date_End)
                                ||
                                (data.Datetime > filterModel.Date_Start && data.Datetime <= filterModel.Date_End)
                                )

                             select new Data_Request
                             {
                                 ID = data.ID,
                                 Count = data.Count,
                                 Timespan = data.Timespan,
                                 IDequipment = data.IDequipment,
                                 Info = data.Info,
                                 Datetime = data.Datetime,
                                 Sended = data.Sended,
                                 Sended_Datetime = data.Sended_Datetime,
                                 Lat = data.Lat,
                                 Lon = data.Lon,
                                 Altitude= data.Altitude,
                                 Ubication = data.Lat + "\" " + data.Lon + "\""
                             }).OrderByDescending(x => x.IDequipment).ToList();

                #region Filtro

                if (lista != null && lista.Count != 0)
                {
                    if (filterModel.IDequipment != null && filterModel.IDequipment.Count != 0)
                    {
                        try
                        {
                            var temp = lista.Where(y => filterModel.IDequipment.Any(z => z == y.IDequipment)).ToList();
                            lista = temp;
                        }
                        catch (Exception)
                        { }
                    }

                    if (filterModel.Sended != null)
                    {
                        try
                        {
                            var temp = lista.Where(x => x.Sended == filterModel.Sended).ToList();
                            lista = temp;
                        }
                        catch (Exception)
                        { }
                    }

                    if (lista != null && lista.Count != 0)
                    {
                        var _listEquipment = (from equipment in db.Equipments
                                              select new Equipment_Request
                                              {
                                                  ID = equipment.ID,
                                                  Name = equipment.Name,
                                                  Type = equipment.Type,
                                              }).ToList();

                        foreach (var item in lista)
                        {
                            var _equipment = _listEquipment.Where(x => x.ID == item.IDequipment).FirstOrDefault();
                            if (_equipment != null)
                            {
                                item.Equipment_ID = _equipment.ID;
                                item.Equipment_Name = _equipment.Name;
                                item.Equipment_Type = _equipment.Type;
                            }
                        }
                    }
                }

                #endregion Filtro

                return lista;
            }
            catch (Exception e)
            {
                LogsClass.NewError(_user, "Could not load list of data", SystemActionsEnum.SearchList, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                throw new Exception("Could not load list of data.");
            }
        }

        #endregion Get


        public static GlobalResponse Create(ClaimsPrincipal _user, Data_Request model)
        {
            using var db = new Local_Context();

            try
            {
                var data = new Data
                {
                    Timespan = model.Timespan,
                    IDequipment = model.IDequipment,
                    Datetime = model.Datetime,
                    Count = model.Count,
                    Info = model.Info,
                    Sended = model.Sended,
                    Sended_Datetime = model.Sended_Datetime,
                    Lat = model.Lat,
                    Lon = model.Lon,
                    Altitude = model.Altitude
                };

                db.Data.Add(data);
                db.SaveChanges();

                var _equipment = (from equipment in db.Equipments
                                  where equipment.ID == data.IDequipment
                                  select new Equipment_Request
                                  {
                                      ID = equipment.ID,
                                      IDstatus = equipment.IDstatus,
                                      Name = equipment.Name,
                                      Type = equipment.Type,

                                      Modify_Date = equipment.Modify_Date,
                                      Modify_IDuser = equipment.Modify_IDuser,

                                      IDstation = equipment.IDstation,
                                      MAC = equipment.MAC,
                                  }).FirstOrDefault();
                if (_equipment != null)
                {
                    model.Equipment_ID = _equipment.ID;
                    model.Equipment_Name = _equipment.Name;
                    model.Equipment_Type = _equipment.Type;
                }

                model.Ubication = data.Lat + "\" " + data.Lon + "\"";


                #region Save move
                Task.Run(async () =>
                {
                    await LogsClass.Create_Data(_user, data);
                });
                #endregion Guardado de movimientos

                return new GlobalResponse(StatusCodes.Status201Created, JsonConvert.SerializeObject(model));
            }
            catch (Exception e)
            {
                LogsClass.NewError(_user, "Could not create the data", SystemActionsEnum.Create, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not create. " + e.Message.ToString() + " DBCreated: " + db.Database.EnsureCreated());
            }
        }

        public static GlobalResponse Delete(ClaimsPrincipal _user)
        {
            using var db = new Local_Context();

            try
            {
                var list = db.Data;
                db.Data.RemoveRange(list);
                db.SaveChanges();

                return new GlobalResponse(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                LogsClass.NewError(_user, "Could not delete data", SystemActionsEnum.Delete, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not delete. " + e.Message.ToString() + " DBCreated: " + db.Database.EnsureCreated());
            }
        }


        public static GlobalResponse RecreateTables(ClaimsPrincipal _user)
        {
            try
            {
                var db = new Local_Context(true);

                return new GlobalResponse(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                LogsClass.NewError(_user, "Could not recreate tables", SystemActionsEnum.Delete, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not recreate tables. " + e.Message.ToString());
            }
        }


    }
}
