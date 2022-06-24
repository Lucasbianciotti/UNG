using APIClient.LocalClass;
using CommonModels.Request;
using Microsoft.EntityFrameworkCore;
using Models.Enums;
using Models.Request;
using Newtonsoft.Json;
using System.Security.Claims;

namespace APIClient.LocalModels.SQLite
{
    public partial class Local_Context : DbContext
    {

        public readonly string database = "dbSqlite.db";
        public string DbPath { get; set; }

        public Local_Context(bool _recreateTables = false)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, database);

            try
            {
                Database.EnsureCreated();

                if (_recreateTables)
                {
                    _recreateTables = false;
                    Database.EnsureDeleted();
                    Database.EnsureCreated();

                    VerifyData();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void VerifyData()
        {
            _CreateClient();
            _CreateStation();
            _CreateUser();
        }


        private void _CreateClient()
        {
            try
            {
                var client = ClientsClass.SearchClient(new ClaimsPrincipal());

                if (client == null)
                {
                    ClientsClass.Create(new ClaimsPrincipal(), new Client_Request()
                    {
                        IDstatus = ClientsStatusEnum.Enabled,
                        IDung = 1,
                        Modify_Date = DateTime.Now,
                        Modify_IDuser = 1,
                        Name = "Client",
                    });

                    client = ClientsClass.SearchClient(new ClaimsPrincipal());
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void _CreateStation()
        {
            try
            {
                var station = StationsClass.SearchStation(new ClaimsPrincipal(), "");
                if (station == null)
                {
                    StationsClass.Create(new ClaimsPrincipal(), new Station_Request()
                    {
                        IDung = 1,
                        IDclient = 1,
                        Modify_IDuser = 1,
                        Modify_Date = DateTime.Now,
                        IDstatus = StationStatusEnum.Enabled,
                        Name = "Station 1",
                        Location = "Principal",
                        Location_GPS_Lat = 000000,
                        Location_GPS_Lon = 000000,
                        Host = "http://",
                        IP_Private = "192168120044",
                        Port = 8080,
                        SSID_Int = "Nexus",
                        PASS_Int = "Nexus2022",
                        PASS_Int_SecurityType = 3,

                        IP_Public = "111111111111",
                        SSID_Ext = "Test",
                        PASS_Ext = "Test",
                        PASS_Ext_SecurityType = 3,

                        Map = new Map_Request()
                        {
                            //Image = new ImageFile_Request()
                            //{
                            //    SRC = "data/map.png",
                            //    File_Name = "map.png",
                            //},
                            Map_BottomLeft_Lat = -31.4169927,
                            Map_BottomLeft_Lon = -63.0119530,

                            Map_TopRight_Lat = -31.4161650,
                            Map_TopRight_Lon = -63.0097930,
                        }
                    });

                    station = StationsClass.SearchStation(new ClaimsPrincipal(), "");
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void _CreateUser()
        {
            try
            {
                var users = UsersClass.CompleteInformation(new ClaimsPrincipal(), new Filter_Request(), "").ListOfUsers;
                if (users == null || users.Count == 0)
                {
                    var listPermission = new List<Permissions_Request>();
                    foreach (int indice in Enum.GetValues(typeof(SystemSectionsEnum)))
                    {
                        listPermission.Add(new Permissions_Request()
                        {
                            IDsection = indice,
                            Read = true,
                            Create = true,
                            Delete = true,
                            Export = true,
                            Modify = true,
                        });
                    }

                    UsersClass.Create(new ClaimsPrincipal(), new User_Request()
                    {
                        IDung = 1,
                        Email = "test@gmail.com",
                        Password = "asd123",
                        IDrole = (int)RolesEnum.ClientAdmin,
                        Name = "User",
                        Surname = "Admin",
                        JSONListOfPermissions = JsonConvert.SerializeObject(listPermission),
                    });

                    users = UsersClass.CompleteInformation(new ClaimsPrincipal(), new Filter_Request(), "").ListOfUsers;
                }
            }
            catch (Exception ex)
            {

            }
        }



        public Local_Context(DbContextOptions<Local_Context> options)
            : base(options)
        {
        }


        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Data> Data { get; set; }
        public virtual DbSet<DataSended> DataSended { get; set; }
        public virtual DbSet<Equipments> Equipments { get; set; }
        public virtual DbSet<Maps> Maps { get; set; }
        public virtual DbSet<Logs_Errors> Logs_Errors { get; set; }
        public virtual DbSet<Logs_SystemMoves> Logs_SystemMoves { get; set; }
        public virtual DbSet<Stations> Stations { get; set; }
        public virtual DbSet<Users> Users { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        var dataSource = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), database);
        //        optionsBuilder.UseSqlite($"Data Source={dataSource};");
        //    }

        //    base.OnConfiguring(optionsBuilder);
        //}



        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Clients>(entity =>
            {

            });

            modelBuilder.Entity<Data>(entity =>
            {

            });

            modelBuilder.Entity<DataSended>(entity =>
            {

            });

            modelBuilder.Entity<Equipments>(entity =>
            {

            });

            modelBuilder.Entity<Maps>(entity =>
            {

            });

            modelBuilder.Entity<Logs_Errors>(entity =>
            {

            });

            modelBuilder.Entity<Logs_SystemMoves>(entity =>
            {

            });

            modelBuilder.Entity<Stations>(entity =>
            {

            });

            modelBuilder.Entity<Users>(entity =>
            {

            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
