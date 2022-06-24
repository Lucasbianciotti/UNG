using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIClient.LocalModels.SQLite
{
    [Table("Stations")]
    public partial class Stations
    {

        [Key]
        public long ID { get; set; }

        public long IDung { get; set; }

        public long IDclient { get; set; }


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


        [Column(TypeName = "datetime")]
        public DateTime Modify_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string IDstatus { get; set; }


        [Required]
        [StringLength(50)]
        public string Name { get; set; }


        [Required]
        [StringLength(150)]
        public string Location { get; set; }


        public long? Location_GPS_Lat { get; set; }

        public long? Location_GPS_Lon { get; set; }


        [Required]
        [StringLength(250)]
        public string Host { get; set; }


        [Required]
        [StringLength(20)]
        public string IP_Private { get; set; }


        public int Port { get; set; }


        [Required]
        [StringLength(150)]
        public string SSID_Int { get; set; }


        [Required]
        [StringLength(150)]
        public string PASS_Int { get; set; }

        public int PASS_Int_SecurityType { get; set; }



        [StringLength(20)]
        public string IP_Public { get; set; }


        [StringLength(150)]
        public string SSID_Ext { get; set; }


        [StringLength(150)]
        public string PASS_Ext { get; set; }

        public int? PASS_Ext_SecurityType { get; set; }




        //public byte[] Map_Bytes { get; set; }

        public double Map_TopRight_Lat { get; set; }
        public double Map_TopRight_Lon { get; set; }
        public double Map_BottomLeft_Lat { get; set; }
        public double Map_BottomLeft_Lon { get; set; }
    }
}