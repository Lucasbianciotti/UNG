﻿namespace Models.Global
{
    public class ImageFile
    {
        public long ID { get; set; }
        public string IDestado { get; set; }

        public string SRC_Imagen { get; set; }

        public string UbicacionDeGuardado { get; set; }


        public byte[] File_Content { get; set; }
        public string File_Name { get; set; }
    }
}
