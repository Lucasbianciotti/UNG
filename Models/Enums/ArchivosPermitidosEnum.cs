using System.Collections.Generic;

namespace Models.Enums
{
    public static class ArchivosPermitidosEnum
    {
        private static readonly List<string> FormatosDeImagen = new()
        {
            "jpg",
            "jpeg",
            "png",
        };

        private static readonly List<string> FormatosDeDocumentos = new()
        {
            "pdf",
            "doc",
            "docx",
        };

        private static readonly List<string> FormatosDeVideos = new()
        {
            "mp4",
        };


        public static bool ExisteFormatoDeImagen(string item)
        {
            return FormatosDeImagen.Exists(x => x.ToLower() == item.Split('/')[1].ToLower());
        }

        public static bool ExisteFormatoDeDocumento(string item)
        {
            var formato = item.Split('/')[1].ToLower();
            var existe = FormatosDeDocumentos.Exists(x=>x.ToLower() == formato);
            return existe;
        }

        public static bool ExisteFormatoDeVideo(string item)
        {
            return FormatosDeVideos.Exists(x => x.ToLower() == item.Split('/')[1].ToLower());
        }
    }
}
