using Models.EntityFrameworks;
using Models.Enums;
using Models.Request;
using System.Diagnostics;
using System.Security.Claims;

namespace API.LocalClass
{
    public static class Logs_ErroresClass
    {

        public static void NuevoLog(ClaimsPrincipal user, string comentario,
            AccionesDelSistemaEnum accion, TiposDeSistemaEnum sistema, Exception exception, CodigosDeErrorEnum codigo)
        {
            Task.Run(async () =>
            {
                using var db = new UNG_Context();

                try
                {
                    var error = new Logs_Errores
                    {
                        IDusuario = UsuariosClass.GetID_User(user),
                        IDempresa = UsuariosClass.GetID_Company(user),
                        Fecha = DateTime.Now,
                        Sistema = sistema.ToString(),
                        Codigo = codigo.ToString(),
                        Comentario = comentario,
                        Aux = null,
                        Accion = accion.ToString(),
                    };

                    if (exception != null)
                    {
                        try
                        {
                            var st = new StackTrace(exception, true);
                            var frame = st.GetFrames()
                                          .Select(frame => new
                                          {
                                              FileName = frame.GetFileName(),
                                              LineNumber = frame.GetFileLineNumber(),
                                              ColumnNumber = frame.GetFileColumnNumber(),
                                              Method = frame.GetMethod(),
                                              Class = frame.GetMethod().DeclaringType,
                                          }).FirstOrDefault();

                            error.Excepcion = exception.ToString();
                            error.Excepcion_Archivo = frame.FileName;
                            error.Excepcion_Metodo = frame.Class.ToString() + " - " + frame.Method.ToString();
                            error.Excepcion_NumeroDeLinea = frame.LineNumber.ToString();
                            error.Excepcion_Source = exception.Source;
                            if (exception.InnerException != null)
                                error.Excepcion_Mensaje = exception.InnerException.ToString();
                        }
                        catch (Exception)
                        {

                        }
                    }

                    db.Logs_Errores.Add(error);
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {

                }

            });
        }


        public static void NuevoLog(ClaimsPrincipal user, New_Error_Request new_Error_Request)
        {
            NuevoLog(user, new_Error_Request.Comentario, new_Error_Request.Accion, new_Error_Request.Sistema, new_Error_Request.Excepcion, new_Error_Request.Codigo);
        }
    }
}
