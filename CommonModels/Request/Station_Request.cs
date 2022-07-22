using CommonCommonModels.Request;
using System.ComponentModel.DataAnnotations;

namespace CommonModels.Request
{

    public class InformationOfStation_Request
    {
        public InformationOfStation_Request()
        {
            ListOfStations = new();
            Filters = new();
        }

        public Filter_Request Filters { get; set; }
        public List<Station_Request> ListOfStations { get; set; }

    }

    public class Station_Request
    {
        public Station_Request()
        {
            ListOfEquipments = new();
            Modify_Date = DateTime.Now;

            InternetConnected = true;

            Drone_Charging = true;
            Drone_Voltage = (decimal)5.2;

            Station_Charging = true;
            Station_MinVoltage = (decimal)8;
            Station_Voltage = (decimal)13.5;

            Station_ShutdownScreen = 60;

            ShowQR = true;

            IP_Private = "192.168.30.10";


            Map = new Map_Request();
        }

        [Key]
        public long ID { get; set; }
        public long IDung { get; set; }
        public long IDclient { get; set; }


        public Map_Request Map { get; set; }


        public bool ShowQR { get; set; }


        public bool InternetConnected { get; set; }

        public bool Drone_Charging { get; set; }
        public decimal Drone_Voltage { get; set; }

        public bool Station_Charging { get; set; }
        public decimal Station_Voltage { get; set; }


        [Required(ErrorMessage = "The minimum voltage is required.")]
        public decimal Station_MinVoltage { get; set; }

        [Required(ErrorMessage = "The time to shutdown screen is required.")]
        public int Station_ShutdownScreen { get; set; }


        public long Modify_IDuser { get; set; }
        public DateTime Modify_Date { get; set; }
        public string IDstatus { get; set; }



        [Required(ErrorMessage = "The name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The host is required.")]
        public string Host { get; set; }



        [Required(ErrorMessage = "The location is required.")]
        public string Location { get; set; }

        public long Location_GPS_Lat { get; set; }
        public long Location_GPS_Lon { get; set; }



        [Required(ErrorMessage = "The port is required.")]
        [Range(1, 9999, ErrorMessage = "The port is invalid.")]
        public int Port { get; set; }

        [Required(ErrorMessage = "The IP is required.")]
        public string IP_Private { get; set; }


        [Required(ErrorMessage = "The SSID is required.")]
        public string SSID_Int { get; set; }

        [Required(ErrorMessage = "The password is required.")]
        public string PASS_Int { get; set; }

        [Required(ErrorMessage = "The security type is required.")]
        public int? PASS_Int_SecurityType { get; set; }


        public string? IP_Public { get; set; }
        public string? SSID_Ext { get; set; }
        public string? PASS_Ext { get; set; }
        public int? PASS_Ext_SecurityType { get; set; }




        public int CountEquipments { get; set; }
        public int CountEquipmentsActives { get; set; }
        public List<Equipment_Request> ListOfEquipments { get; set; }

    }

    public class DeleteStation_Request
    {
        public DeleteStation_Request()
        {
            Filters = new();
        }

        [Required]
        public long ID { get; set; }
        public Filter_Request Filters { get; set; }

    }

}
