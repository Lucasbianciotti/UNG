using APIClient.LocalModels.SQLite;
using Class;
using CommonClass;
using CommonModels.Request;
using CommonModels.Enums;
using CommonModels.Global;
using System.Security.Claims;

namespace APIClient.LocalClass
{
    public static class StationsClass
    {
        #region Get
        public static LocalResponse_Request CompleteInformation(ClaimsPrincipal _user, string IP)
        {
            var data = new LocalResponse_Request(ClientsClass.SearchClient(_user), StationsClass.SearchStation(_user, IP), UsersClass.SearchUser(_user))
            {
                ListOfData = DataClass.ListOfData(_user, new FilterData_Request()
                {
                    Date_Start = DateTime.Now - TimeSpan.FromDays(7),
                    Date_End = DateTime.Now
                })
            };

            return data;
        }
        public static LocalResponse_Request CompleteInformation(ClaimsPrincipal _user, string IP, FilterDashboard_Request _filters)
        {
            var data = new LocalResponse_Request(ClientsClass.SearchClient(_user), StationsClass.SearchStation(_user, IP), UsersClass.SearchUser(_user))
            {
                ListOfData = DataClass.ListOfData(_user, new FilterData_Request()
                {
                    Date_Start = _filters.Date_Start.Value,
                    Date_End = _filters.Date_End.Value
                })
            };

            return data;
        }

        public static Station_Request SearchStation(ClaimsPrincipal _user, string IP)
        {
            using var db = new Local_Context();

            try
            {
                var _station = (from station in db.Stations

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

                                    IDclient = station.IDclient,
                                    IDung = station.IDung,

                                    Modify_Date = station.Modify_Date,
                                    Modify_IDuser = station.Modify_IDuser,

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

                                    Map = new Map_Request()
                                    {
                                        Map_BottomLeft_Lat = station.Map_BottomLeft_Lat,
                                        Map_BottomLeft_Lon = station.Map_BottomLeft_Lon,
                                        Map_TopRight_Lat = station.Map_TopRight_Lat,
                                        Map_TopRight_Lon = station.Map_TopRight_Lon,

                                        File_Content = db.Maps.FirstOrDefault() != null ? db.Maps.FirstOrDefault().Content : null,
                                        File_Name = db.Maps.FirstOrDefault() != null ? db.Maps.FirstOrDefault().FileName : null,
                                        File_ContentType = db.Maps.FirstOrDefault() != null ? db.Maps.FirstOrDefault().ContentType : null,
                                    }

                                }).FirstOrDefault();


                if (_station == null) return null;


                _station.ListOfEquipments = (from Equipment in db.Equipments

                                             where Equipment.IDstatus != EquipmentStatusEnum.Deleted
                                             && Equipment.IDstation == _station.ID

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
                                             }).OrderByDescending(x => x.Name).ToList();

                if (_station.ListOfEquipments != null)
                {
                    _station.CountEquipments = _station.ListOfEquipments.Count;
                    var tempList = _station.ListOfEquipments.Where(x => x.IDstatus == EquipmentStatusEnum.Active).ToList();
                    if (tempList != null)
                        _station.CountEquipmentsActives = tempList.Count;
                }
                return _station;
            }
            catch (Exception e)
            {
                LogsClass.NewError(_user, "Could not load information's stations", SystemActionsEnum.SearchList, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                throw new Exception("Could not load information's stations.");
            }
        }

        public static long SearchIDStation(ClaimsPrincipal _user)
        {
            using var db = new Local_Context();

            try
            {
                var _station = db.Stations.FirstOrDefault();
                if (_station == null) return 0;

                return _station.ID;
            }
            catch (Exception e)
            {
                LogsClass.NewError(_user, "Could not load information's stations", SystemActionsEnum.SearchList, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                throw new Exception("Could not load information's stations.");
            }
        }
        #endregion Get


        public static GlobalResponse Create(ClaimsPrincipal _user, Station_Request model)
        {
            using var db = new Local_Context();

            try
            {
                var station = new Stations
                {
                    Modify_Date = DateTime.Now,
                    Modify_IDuser = GlobalClass.GetID_User(_user),
                    IDclient = model.IDclient,
                    IDung = model.IDung,
                    Name = model.Name,
                    Host = model.Host,
                    IDstatus = model.IDstatus,
                    IP_Private = model.IP_Private, //ConvertToIPClass.AddDots(model.IP_Private),
                    Port = model.Port,
                    SSID_Int = model.SSID_Int,
                    PASS_Int = model.PASS_Int,
                    PASS_Int_SecurityType = model.PASS_Int_SecurityType.Value,
                    Location = model.Location,
                    Location_GPS_Lat = model.Location_GPS_Lat,
                    Location_GPS_Lon = model.Location_GPS_Lon,
                    IP_Public = model.IP_Public, //ConvertToIPClass.AddDots(model.IP_Public),
                    SSID_Ext = model.SSID_Ext,
                    PASS_Ext = model.PASS_Ext,
                    PASS_Ext_SecurityType = model.PASS_Ext_SecurityType,

                    InternetConnected = model.InternetConnected,

                    Drone_Charging = model.Drone_Charging,
                    Drone_Voltage = model.Drone_Voltage,

                    Station_Charging = model.Station_Charging,
                    Station_Voltage = model.Station_Voltage,
                    Station_MinVoltage = model.Station_MinVoltage,

                    Station_ShutdownScreen = model.Station_ShutdownScreen,

                    ShowQR = model.ShowQR,

                    Map_BottomLeft_Lat = model.Map.Map_BottomLeft_Lat,
                    Map_BottomLeft_Lon = model.Map.Map_BottomLeft_Lon,

                    Map_TopRight_Lat = model.Map.Map_TopRight_Lat,
                    Map_TopRight_Lon = model.Map.Map_TopRight_Lon,
                };

                db.Stations.Add(station);


                var _image = new Maps()
                {
                    Content = model.Map.File_Content,
                    FileName = "map" + Path.GetExtension(model.Map.File_Name),
                    ContentType = model.Map.File_ContentType
                };

                db.Maps.Add(_image);

                db.SaveChanges();


                #region Save move
                Task.Run(async () =>
                {
                    await LogsClass.Create_Station(_user, station);
                });
                #endregion Guardado de movimientos


                return new GlobalResponse(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                LogsClass.NewError(_user, "Could not create the station", SystemActionsEnum.Create, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not create. " + e.Message.ToString());
            }
        }

        public static GlobalResponse Modify(ClaimsPrincipal _user, Station_Request model, string _url)
        {
            using var db = new Local_Context();

            try
            {
                var station = db.Stations.Find(model.ID);
                if (station == null)
                    throw new Exception("Not found station");

                #region Modificar
                station.Modify_Date = DateTime.Now;
                station.Modify_IDuser = GlobalClass.GetID_User(_user);
                station.Name = model.Name;

                station.InternetConnected = model.InternetConnected;

                station.Drone_Charging = model.Drone_Charging;
                station.Drone_Voltage = model.Drone_Voltage;

                station.Station_Charging = model.Station_Charging;
                station.Station_Voltage = model.Station_Voltage;
                station.Station_MinVoltage = model.Station_MinVoltage;

                station.Station_ShutdownScreen = model.Station_ShutdownScreen;

                station.ShowQR = model.ShowQR;

                station.IDstatus = model.IDstatus;
                station.IDclient = model.IDclient;
                station.IP_Private = model.IP_Private;// ConvertToIPClass.AddDots(model.IP_Private);
                station.Port = model.Port;
                station.Host = model.Host;
                station.IP_Public = model.IP_Public;// ConvertToIPClass.AddDots(model.IP_Public);
                station.Location = model.Location;
                station.Location_GPS_Lat = model.Location_GPS_Lat;
                station.Location_GPS_Lon = model.Location_GPS_Lon;
                station.SSID_Ext = model.SSID_Ext;
                station.PASS_Ext = model.PASS_Ext;
                station.PASS_Ext_SecurityType = model.PASS_Ext_SecurityType;
                station.SSID_Int = model.SSID_Int;
                station.PASS_Int = model.PASS_Int;
                station.PASS_Int_SecurityType = model.PASS_Int_SecurityType.Value;

                station.Map_BottomLeft_Lat = model.Map.Map_BottomLeft_Lat;
                station.Map_BottomLeft_Lon = model.Map.Map_BottomLeft_Lon;

                station.Map_TopRight_Lat = model.Map.Map_TopRight_Lat;
                station.Map_TopRight_Lon = model.Map.Map_TopRight_Lon;


                db.Entry(station).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();


                if (model.Map.File_Content != null)
                {
                    var _image = db.Maps.FirstOrDefault();

                    if (_image == null)
                        _image = new Maps();

                    _image.Content = model.Map.File_Content;
                    _image.FileName = "map" + Path.GetExtension(model.Map.File_Name);
                    _image.ContentType = model.Map.File_ContentType;

                    db.Entry(_image).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                }
                #endregion Modificar

                #region Guardado de movimientos
                Task.Run(async () =>
                {
                    await LogsClass.Modify_Station(_user, station);
                });
                #endregion Guardado de movimientos

                return new GlobalResponse(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                LogsClass.NewError(_user, "Could not modify station", SystemActionsEnum.Modify, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not modify. " + e.Message.ToString());
            }
        }


        //public static GlobalResponse Delete(ClaimsPrincipal _user, DeleteStation_Request model)
        //{
        //    using var db = new Local_Context();
        //    using var transaction = db.Database.BeginTransaction();

        //    try
        //    {
        //        var station = db.Stations.Find(model.ID);
        //        if (station == null)
        //            throw new Exception("Not found station");


        //        #region Modificacion del station
        //        station.Modify_Date = DateTime.Now;
        //        station.Modify_IDuser = GlobalClass.GetID_User(_user);
        //        station.IDstatus = StationStatusEnum.Deleted;
        //        db.Entry(station).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        db.SaveChanges();
        //        #endregion Modificacion del station

        //        transaction.Commit();

        //        #region Guardado de movimientos
        //        Task.Run(async () =>
        //        {
        //            await LogsClass.Delete_Station(_user, station);
        //        });
        //        #endregion Guardado de movimientos

        //        return new GlobalResponse(StatusCodes.Status200OK);

        //    }
        //    catch (Exception e)
        //    {
        //        transaction.Rollback();

        //        LogsClass.NewError(_user, "Could not delete station", SystemActionsEnum.Delete, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

        //        return new GlobalResponse(StatusCodes.Status500InternalServerError, "Could not delete. " + e.Message.ToString());
        //    }
        //}

    }
}
