using APIClient.LocalModels.SQLite;
using CommonModels.Request;
using Models.Enums;
using System.Security.Claims;

namespace APIClient.LocalClass
{
    public class FilesClass
    {

        //private readonly static string _Path = $"\\data\\maps";


        //public static string Save(ClaimsPrincipal user, ImageFile_Request archivo)
        //{
        //    try
        //    {
        //        //archivo.File_Name = Nombre(archivo);

        //        var url = Url(archivo, _Path + $"\\");

        //        return Save(archivo, url);
        //    }
        //    catch (Exception)
        //    {
        //        return string.Empty;
        //    }
        //}

        public static string SaveMap(ClaimsPrincipal user, Local_Context db, ImageFile_Request image)
        {
            try
            {

                var _image = new Maps()
                {
                    Type = TypeFileSaveEnum.Map.ToString(),
                    Content = image.File_Content,
                    FileName = "map" + Path.GetExtension(image.File_Name),
                };


                //File.Delete($"D:\\Desarrollo\\Proyectos\\UNG\\Client\\wwwroot\\data\\maps\\map.png");

                //File.Delete(SRCImageOld);




                //return Save(image, url);


                var fs = File.Create(url);
                fs.Write(image.File_Content, 0, image.File_Content.Length);
                fs.Close();

                return url;


                //return "";

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }


        private byte[] GetImageBytes(Stream stream)
        {
            byte[] ImageBytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }
            return ImageBytes;
        }

        private Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }



        //public static string SRCfromFile(string URL_DondeSeGuardo)
        //{

        //    URL_DondeSeGuardo = URL_DondeSeGuardo.Replace(@"\", "/"); 

        //    int indice = URL_DondeSeGuardo.IndexOf("data");

        //    string url = URL_DondeSeGuardo.Remove(0, indice);

        //    return url;
        //}

        //private static string Url(ImageFile_Request archivo, string url)
        //{
        //    var path = $"{Directory.GetCurrentDirectory()}{url}";

        //    if (!Directory.Exists(path))
        //        Directory.CreateDirectory(path);

        //    return Path.Combine(path, archivo.File_Name);
        //}

        //private static string Save(ImageFile_Request archivo, string url)
        //{
        //    try
        //    {
        //        var fs = File.Create(url);
        //        fs.Write(archivo.File_Content, 0, archivo.File_Content.Length);
        //        fs.Close();

        //        return url;
        //    }
        //    catch (Exception e)
        //    {
        //        return "";
        //    }
        //}
    }
}
