using Models;
using Models.Enums;
using Models.Request;
using System.Net.Http.Json;

namespace Web.LocalClass
{
    public static class Logs_ErroresClass
    {
        public static void NuevoLog(HttpClient Http, string comentario, SystemActionsEnum accion, SystemTypesEnum sistema, Exception exception, SystemErrorCodesEnum codigo)
        {
            if (exception != null)
            {
                try
                {
                    var model = new New_Error_Request()
                    {
                        Comentario = comentario,
                        Excepcion = exception,
                        Accion = accion,
                        Sistema = sistema,
                        Codigo = codigo
                    };

                    Task.Run(async () =>
                    {
                        var post = await Http.PostAsJsonAsync(URLs.Errores, model);
                    });
                }
                catch (Exception)
                {

                }
            }
        }

        //public static void EnviarLog(New_Error_Request model)
        //{

        //    //if (model.Excepcion != null)
        //    //{
        //    //    try
        //    //    {
        //    //        var st = new StackTrace(model.Excepcion, true);
        //    //        var frame = st.GetFrames()
        //    //                      .Select(frame => new
        //    //                      {
        //    //                          FileName = frame.GetFileName(),
        //    //                          LineNumber = frame.GetFileLineNumber(),
        //    //                          ColumnNumber = frame.GetFileColumnNumber(),
        //    //                          Method = frame.GetMethod(),
        //    //                          Class = frame.GetMethod().DeclaringType,
        //    //                      }).FirstOrDefault();


        //    //        model.Excepcion_Archivo = frame.FileName;
        //    //        model.Excepcion_Metodo = frame.Class.ToString() + " - " + frame.Method.ToString();
        //    //        model.Excepcion_NumeroDeLinea = frame.LineNumber;
        //    //        model.Excepcion_Source = model.Excepcion.Source;
        //    //        model.Excepcion_Mensaje = model.Excepcion.InnerException.ToString();
        //    //        model.Excepcion = null;
        //    //    }
        //    //    catch (Exception)
        //    //    {

        //    //    }
        //    //}

        //    //try
        //    //{
        //    //    Task.Run(async () =>
        //    //    {
        //    //        var post = await _HttpClient.PostAsJsonAsync<New_Error_Request>(URLs.Errores, model);
        //    //    });
        //    //}
        //    //catch (Exception)
        //    //{

        //    //}
        //}

    }
}
