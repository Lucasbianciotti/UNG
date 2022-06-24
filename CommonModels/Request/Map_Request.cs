﻿namespace CommonModels.Request
{
    public class Map_Request
    {
        
        public double Data_Lat { get; set; }
        public double Data_Lon { get; set; }


        public double Map_TopRight_Lat { get; set; }
        public double Map_TopRight_Lon { get; set; }
        public double Map_BottomLeft_Lat { get; set; }
        public double Map_BottomLeft_Lon { get; set; }


        public byte[]? File_Content { get; set; }

        public string File_ContentType { get; set; }

        public string? File_Name { get; set; }


        //public string SRC { get; set; }

        //public ImageFile_Request Image { get; set; }

    }
}