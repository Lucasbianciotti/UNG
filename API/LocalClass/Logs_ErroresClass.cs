using Models.EntityFrameworks;
using Models.Enums;
using Models.Request;
using System.Diagnostics;
using System.Security.Claims;

namespace API.LocalClass
{
    public static class Logs_ErroresClass
    {

        public static void NuevoLog(ClaimsPrincipal _user, string comentario,
            SystemActionsEnum accion, SystemTypesEnum sistema, Exception exception, SystemErrorCodesEnum codigo)
        {
            Task.Run(async () =>
            {
                using var db = new UNG_Context();

                try
                {
                    var error = new Logs_Errors
                    {
                        IDuser = UsersClass.GetID_User(_user),
                        IDclient = UsersClass.GetID_Client(_user),
                        Date = DateTime.Now,
                        System = sistema.ToString(),
                        Code = codigo.ToString(),
                        Commentary = comentario,
                        Aux = null,
                        Action = accion.ToString(),
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

                            error.Exception = exception.ToString();
                            error.Exception_File = frame.FileName;
                            error.Exception_Method = frame.Class.ToString() + " - " + frame.Method.ToString();
                            error.Exception_NumberOfLine = frame.LineNumber.ToString();
                            error.Exception_Source = exception.Source;
                            if (exception.InnerException != null)
                                error.Exception_Message = exception.InnerException.Message.ToString();
                        }
                        catch (Exception)
                        {

                        }
                    }

                    db.Logs_Errors.Add(error);
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {

                }

            });
        }


        public static void NuevoLog(ClaimsPrincipal _user, New_Error_Request new_Error_Request)
        {
            NuevoLog(_user, new_Error_Request.Comentario, new_Error_Request.Accion, new_Error_Request.Sistema, new_Error_Request.Excepcion, new_Error_Request.Codigo);
        }
    }
}
