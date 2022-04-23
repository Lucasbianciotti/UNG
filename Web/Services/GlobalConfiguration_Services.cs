using Microsoft.AspNetCore.Components;
using Models.Enums;
using Models.Request;
using Syncfusion.Blazor.Popups;

namespace Web.Services
{

    public class GlobalConfiguration_Services : IGlobalConfiguration_Services
    {
        [Inject]
        private HttpClient HttpClient { get; set; }
        public GlobalConfiguration_Services(HttpClient client)
        {
            HttpClient = client;
        }


        public GlobalConfiguration_Services()
        {
            Notify_ElementsDataChanged();
        }


        DialogEffect IGlobalConfiguration_Services.AnimationEffect { get => DialogEffect.FadeZoom; }
        int IGlobalConfiguration_Services.AnimationTime { get => 650; }


        public event Action OnChange;
        public void Notify_ElementsDataChanged()
        {
            OnChange?.Invoke();
        }


        public void NuevoLog(string comentario, AccionesDelSistemaEnum accion, Exception exception, CodigosDeErrorEnum codigo)
        {
            //Task.Run(async () =>
            //{
            var error = new New_Error_Request()
            {
                Comentario = comentario,
                Excepcion = exception,
                Accion = accion,
                Sistema = TiposDeSistemaEnum.WEB,
                Codigo = codigo
            };

            //using var db = new MODDY_DevContext();

            //try
            //{
            //    var error = new Logs_Errores
            //    {
            //        IDusuario = UsuariosClass.GetID_User(user),
            //        IDcompania = UsuariosClass.GetID_Company(user),
            //        Fecha = DateTime.Now,
            //        Sistema = sistema.ToString(),
            //        Codigo = codigo.ToString(),
            //        Comentario = comentario,
            //        Aux = null,
            //        Accion = accion.ToString(),
            //    };

            //    if (exception != null)
            //    {
            //        try
            //        {
            //            var st = new StackTrace(exception, true);
            //            var frame = st.GetFrames()
            //                          .Select(frame => new
            //                          {
            //                              FileName = frame.GetFileName(),
            //                              LineNumber = frame.GetFileLineNumber(),
            //                              ColumnNumber = frame.GetFileColumnNumber(),
            //                              Method = frame.GetMethod(),
            //                              Class = frame.GetMethod().DeclaringType,
            //                          }).FirstOrDefault();

            //            error.Excepcion = exception.ToString();
            //            error.Excepcion_Archivo = frame.FileName;
            //            error.Excepcion_Metodo = frame.Class.ToString() + " - " + frame.Method.ToString();
            //            error.Excepcion_NumeroDeLinea = frame.LineNumber;
            //            error.Excepcion_Source = exception.Source;
            //            if (exception.InnerException != null)
            //                error.Excepcion_Mensaje = exception.InnerException.ToString();
            //        }
            //        catch (Exception)
            //        {

            //        }
            //    }

            //    db.Logs_Errores.Add(error);
            //    await db.SaveChangesAsync();
            //}
            //catch (Exception)
            //{

            //}

            //});
        }

        public void EnviarLogDeError(New_Error_Request model)
        {

            //if (model.Excepcion != null)
            //{
            //    try
            //    {
            //        var st = new StackTrace(model.Excepcion, true);
            //        var frame = st.GetFrames()
            //                      .Select(frame => new
            //                      {
            //                          FileName = frame.GetFileName(),
            //                          LineNumber = frame.GetFileLineNumber(),
            //                          ColumnNumber = frame.GetFileColumnNumber(),
            //                          Method = frame.GetMethod(),
            //                          Class = frame.GetMethod().DeclaringType,
            //                      }).FirstOrDefault();


            //        model.Excepcion_Archivo = frame.FileName;
            //        model.Excepcion_Metodo = frame.Class.ToString() + " - " + frame.Method.ToString();
            //        model.Excepcion_NumeroDeLinea = frame.LineNumber;
            //        model.Excepcion_Source = model.Excepcion.Source;
            //        model.Excepcion_Mensaje = model.Excepcion.InnerException.ToString();
            //        model.Excepcion = null;
            //    }
            //    catch (Exception)
            //    {

            //    }
            //}

            //try
            //{
            //    Task.Run(async () =>
            //    {
            //        var post = await _HttpClient.PostAsJsonAsync<New_Error_Request>(URLs.Errores, model);
            //    });
            //}
            //catch (Exception)
            //{

            //}
        }
    }
}
