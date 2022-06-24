using APIAdmin.LocalModels.EntityFrameworks;
using Class;
using Models.Enums;
using Models.Global;
using Models.Request;
using System.Security.Claims;

namespace APIAdmin.LocalClass
{
    public static class StationsClass
    {
        #region Get
        public static GlobalResponse_Request CompleteInformation(ClaimsPrincipal _user, Filter_Request Filtros)
        {
            var data = new GlobalResponse_Request(UsersClass.JSONListOfPermissions(_user))
            {
                InformationOfStations = new()
            };

            data.InformationOfStations.ListOfStations = ListOfStations(_user, Filtros);

            if (data.InformationOfStations.ListOfStations != null && data.InformationOfStations.ListOfStations.Count != 0)
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
                InformationOfStations = new()
            };

            data.InformationOfStations.ListOfStations = ListOfStations(_user, Filtros);

            return data;
        }

        private static List<Station_Request> ListOfStations(ClaimsPrincipal _user, Filter_Request Filtros)
        {
            using var db = new UNG_Context();

            try
            {
                var lista = (from station in db.Stations

                                 //join usuario in db.Usuarios
                                 //on station.IDusuario_Creador equals usuario.ID
                                 //into caq
                                 //from usuario in caq.DefaultIfEmpty()

                             where station.IDstatus != StationStatusEnum.Deleted

                             select new Station_Request
                             {
                                 ID = station.ID,
                                 IDstatus = station.IDstatus,
                                 Name = station.Name,

                                 Modify_Date = station.Modify_Date,
                                 Modify_IDuser = station.Modify_IDuser,

                                 IDclient = station.IDclient,

                                 IP_Private = station.IP_Private,
                                 IP_Public = station.IP_Public,

                                 Port = station.Port,
                                 Host = station.Host,
                                 SSID_Int = station.SSID_Int,
                                 PASS_Int = station.PASS_Int,
                                 PASS_Int_SecurityType = station.PASS_Int_SecurityType,

                                 SSID_Ext = station.SSID_Ext,
                                 PASS_Ext = station.PASS_Ext,
                                 PASS_Ext_SecurityType = station.PASS_Ext_SecurityType,

                                 Location = station.Location,
                                 Location_GPS_Lat = station.Location_GPS_Lat ?? 0,
                                 Location_GPS_Lon = station.Location_GPS_Lon ?? 0,

                                 ListOfEquipments = (from Equipment in db.Equipments

                                                     where Equipment.IDstatus != EquipmentStatusEnum.Deleted
                                                     && Equipment.IDstation == station.ID

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
                                                     }).OrderByDescending(x => x.Name).ToList(),

                                 Aux = station.Aux,
                             }).OrderByDescending(x => x.Location).ToList();

                foreach (var item in lista)
                {
                    item.CountEquipments = item.ListOfEquipments.Count;
                }

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
                Logs_ErrorsClass.NuevoLog(_user, "Could not load list of stations", SystemActionsEnum.SearchList, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                throw new Exception("Could not load list of stations.");
            }
        }
        #endregion Get


        public static GlobalResponse Create(ClaimsPrincipal _user, Station_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {

                model.IP_Private = model.IP_Private.Replace(".", "");

                var station = new Stations
                {
                    Modify_Date = DateTime.Now,
                    Modify_IDuser = GlobalClass.GetID_User(_user),
                    IDclient = model.IDclient,
                    Name = model.Name,
                    Host = model.Host,
                    IDstatus = model.IDstatus,
                    IP_Private = model.IP_Private.Substring(0, 3) + "." + model.IP_Private.Substring(3, 3) + "." + model.IP_Private.Substring(6, 3) + "." + model.IP_Private.Substring(9, 3),
                    Port = model.Port,
                    SSID_Int = model.SSID_Int,
                    PASS_Int = model.PASS_Int,
                    PASS_Int_SecurityType = model.PASS_Int_SecurityType.Value,
                    Location = model.Location,
                    Location_GPS_Lat = model.Location_GPS_Lat,
                    Location_GPS_Lon = model.Location_GPS_Lon,
                    IP_Public = model.IP_Public,
                    SSID_Ext = model.SSID_Ext,
                    PASS_Ext = model.PASS_Ext,
                    PASS_Ext_SecurityType = model.PASS_Ext_SecurityType,

                    Aux = model.Aux,
                };


                db.Stations.Add(station);
                db.SaveChanges();

                transaction.Commit();


                #region Save move
                Task.Run(async () =>
                {
                    await Logs_SystemMovesClass.Create_Station(_user, station);
                });
                #endregion Guardado de movimientos

                return new GlobalResponse(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                transaction.Rollback();

                Logs_ErrorsClass.NuevoLog(_user, "Could not create the station", SystemActionsEnum.Create, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not create. " + e.Message.ToString());
            }
        }

        public static GlobalResponse Modify(ClaimsPrincipal _user, Station_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                var station = db.Stations.Find(model.ID);
                if (station == null)
                    throw new Exception("Not found station");

                station.IP_Private = model.IP_Private.Replace(".", "");

                #region Modificar
                station.Modify_Date = DateTime.Now;
                station.Modify_IDuser = GlobalClass.GetID_User(_user);
                station.Name = model.Name;
                station.IDstatus = model.IDstatus;
                station.IP_Private = model.IP_Private.Substring(0, 3) + "." + model.IP_Private.Substring(3, 3) + "." + model.IP_Private.Substring(6, 3) + "." + model.IP_Private.Substring(9, 3);
                station.Port = model.Port;
                station.Host = model.Host;
                station.IP_Public = model.IP_Public;
                station.Location = model.Location;
                station.Location_GPS_Lat = model.Location_GPS_Lat;
                station.Location_GPS_Lon = model.Location_GPS_Lon;
                station.SSID_Ext = model.SSID_Ext;
                station.PASS_Ext = model.PASS_Ext;
                station.PASS_Ext_SecurityType = model.PASS_Ext_SecurityType;
                station.SSID_Int = model.SSID_Int;
                station.PASS_Int = model.PASS_Int;
                station.PASS_Int_SecurityType = model.PASS_Int_SecurityType.Value;
                station.Aux = model.Aux;
                db.Entry(station).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                #endregion Modificar


                transaction.Commit();

                #region Guardado de movimientos
                Task.Run(async () =>
                {
                    await Logs_SystemMovesClass.Modify_Station(_user, station);
                });
                #endregion Guardado de movimientos

                return new GlobalResponse(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                transaction.Rollback();

                Logs_ErrorsClass.NuevoLog(_user, "Could not modify station", SystemActionsEnum.Modify, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not modify. " + e.Message.ToString());
            }
        }

        public static GlobalResponse Delete(ClaimsPrincipal _user, DeleteStation_Request model)
        {
            using var db = new UNG_Context();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                var station = db.Stations.Find(model.ID);
                if (station == null)
                    throw new Exception("Not found station");


                #region Modificacion del station
                station.Modify_Date = DateTime.Now;
                station.Modify_IDuser = GlobalClass.GetID_User(_user);
                station.IDstatus = StationStatusEnum.Deleted;
                db.Entry(station).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                #endregion Modificacion del station

                transaction.Commit();

                #region Guardado de movimientos
                Task.Run(async () =>
                {
                    await Logs_SystemMovesClass.Delete_Station(_user, station);
                });
                #endregion Guardado de movimientos

                return new GlobalResponse(StatusCodes.Status200OK);

            }
            catch (Exception e)
            {
                transaction.Rollback();

                Logs_ErrorsClass.NuevoLog(_user, "Could not delete station", SystemActionsEnum.Delete, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not delete. " + e.Message.ToString());
            }
        }

    }
}
