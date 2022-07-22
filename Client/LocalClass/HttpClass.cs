using Client.Services;
using Models;
using CommonModels.Enums;
using CommonModels.Request;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Client.LocalClass
{
    public static class HttpClass
    {

        //public static async Task<Dashboard_Request> Get_Dashboard(HttpClient Http, FilterDashboard_Request filtros, IToast_Services _Toast)
        //{
        //    try
        //    {
        //        HttpResponseMessage post = await Post(Http, URLsClient.Dashboard, filtros, _Toast);

        //        if (post != null && post.IsSuccessStatusCode)
        //        {
        //            var cadena = await post.Content.ReadAsStringAsync();

        //            var response = JsonConvert.DeserializeObject<Dashboard_Request>(cadena);

        //            return response;
        //        }
        //        else
        //        {
        //            TypeError(_Toast, post);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        HttpClass.NuevoLog(Http, "Could not deserialize.", SystemActionsEnum.GET, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);

        //        _Toast.ShowError("The information could not be loaded.");
        //    }

        //    return null;
        //}

        public static async Task<bool> GetData(HttpClient Http, string URL, IToast_Services _Toast, IGlobalElements_Services _GlobalElements, FilterData_Request filtros)
        {
            try
            {
                LocalResponse_Request response = null;
                response = await PostOnly(Http, URL, filtros, _Toast);

                if (response == null)
                    return false;

                _GlobalElements.SetearInformacion(response);
                return true;
            }
            catch (Exception e)
            {
                HttpClass.NuevoLog(Http, "Could not deserialize.", SystemActionsEnum.GET, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);
                //_Toast.ShowError("The information could not be loaded.");
            }

            return false;
        }


        #region Get

        public static async Task<bool> GetSet_Dashboard(HttpClient Http, string URL, IToast_Services _Toast, IGlobalElements_Services _GlobalElements, FilterDashboard_Request filtros)
        {
            try
            {
                LocalResponse_Request response = null;
                if (filtros != null)
                    response = await PostOnly(Http, URL, filtros, _Toast);
                else
                    response = await GetOnly(Http, URL, _Toast);


                if (response == null)
                    return false;

                _GlobalElements.SetearInformacion(response);
                return true;
            }
            catch (Exception e)
            {
                HttpClass.NuevoLog(Http, "Could not deserialize.", SystemActionsEnum.GET, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);
                //_Toast.ShowError("The information could not be loaded.");
            }

            return false;
        }

        public static async Task<bool> GetSet(HttpClient Http, string URL, IToast_Services _Toast, IGlobalElements_Services _GlobalElements, Filter_Request filtros = null)
        {
            try
            {
                LocalResponse_Request response = null;
                if (filtros != null)
                    response = await PostOnly(Http, URL, filtros, _Toast);
                else
                    response = await GetOnly(Http, URL, _Toast);


                if (response == null)
                    return false;

                _GlobalElements.SetearInformacion(response);
                return true;
            }
            catch (Exception e)
            {
                HttpClass.NuevoLog(Http, "Could not deserialize.", SystemActionsEnum.GET, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);
                //_Toast.ShowError("The information could not be loaded.");
            }

            return false;
        }

        public static async Task<LocalResponse_Request> GetOnly(HttpClient Http, string URL, IToast_Services _Toast, Filter_Request filtros = null, long? ID = null)
        {
            try
            {
                if (filtros != null)
                    return await PostOnly(Http, URL, filtros, _Toast);
                else if (ID != null)
                    return await PostOnly(Http, URL, ID, _Toast);
                else
                {
                    var post = await Get(Http, URL, _Toast);

                    if (post != null && post.IsSuccessStatusCode)
                    {
                        var cadena = await post.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<LocalResponse_Request>(cadena);
                    }
                    else
                    {
                        TypeError(_Toast, post);
                    }
                }
            }
            catch (Exception e)
            {
                HttpClass.NuevoLog(Http, "Could not deserialize.", SystemActionsEnum.GET, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);
                //_Toast.ShowError("The information could not be loaded.");
            }

            return null;
        }
        #endregion Get


        #region Post
        public static async Task<bool> PostSet(HttpClient Http, string URL, object model, IToast_Services _Toast, IGlobalElements_Services _GlobalElements)
        {
            try
            {
                var response = await PostOnly(Http, URL, model, _Toast);
                if (response == null)
                    return false;

                _GlobalElements.SetearInformacion(response);
                return true;
            }
            catch (Exception e)
            {
                HttpClass.NuevoLog(Http, "Could not deserialize.", SystemActionsEnum.POST, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);
                //_Toast.ShowError("The information could not be loaded.");
            }

            return false;
        }

        public static async Task<LocalResponse_Request> PostOnly(HttpClient Http, string URL, object model, IToast_Services _Toast)
        {
            try
            {
                HttpResponseMessage post = await Post(Http, URL, model, _Toast);

                if (post != null && post.IsSuccessStatusCode)
                {
                    var cadena = await post.Content.ReadAsStringAsync();
                    var temp = JsonConvert.DeserializeObject<LocalResponse_Request>(cadena);

                    if (!string.IsNullOrEmpty(temp.Message))
                        _Toast.ShowSuccess(temp.Message, "Atention");

                    return temp;
                }

                TypeError(_Toast, post);
            }
            catch (Exception e)
            {
                HttpClass.NuevoLog(Http, "Could not deserialize.", SystemActionsEnum.POST, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);
                //_Toast.ShowError("The information could not be loaded.");
            }

            return null;
        }
        #endregion Post


        #region Private methods
        private static async Task<HttpResponseMessage> Post(HttpClient Http, string URL, object model, IToast_Services _Toast)
        {
            try
            {
                var strings = JsonConvert.SerializeObject(model);
                //string jsonString = System.Text.Json.JsonSerializer.Serialize(model);

                //var asd = strings.Length;
                //var asdd = jsonString.Length;
                return await Http.PostAsJsonAsync(URL, model);
            }
            catch (Exception e)
            {
                HttpClass.NuevoLog(Http, "Could not connect with server.", SystemActionsEnum.POST, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);
                _Toast.ShowError("Could not connect with server.");
                return null;
            }
        }
        private static async Task<HttpResponseMessage> Get(HttpClient Http, string URL, IToast_Services _Toast)
        {
            try
            {
                return await Http.GetAsync(URL);
            }
            catch (Exception e)
            {
                HttpClass.NuevoLog(Http, "Could not connect with server.", SystemActionsEnum.GET, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);
                _Toast.ShowError("Could not connect with server.");
                return null;
            }
        }

        public static void TypeError(IToast_Services _Toast, HttpResponseMessage post)
        {

            switch (post.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _Toast.ShowWarning("Login, please.");
                    break;
                //return "Login, please.";

                case HttpStatusCode.Forbidden:
                    _Toast.ShowWarning("The user does not have the necessary permissions.");
                    break;
                //return "The user does not have the necessary permissions.";

                case HttpStatusCode.BadRequest:
                    _Toast.ShowError("Could not connect to server.");
                    break;
                //return "Could not connect to server.";

                case HttpStatusCode.NotFound:
                    _Toast.ShowError("The URL was not found.");
                    break;
                //return "The URL was not found.";

                case HttpStatusCode.InternalServerError:
                    _Toast.ShowError("Server error.");
                    break;
                //return "Server error.";

                case HttpStatusCode.Continue:
                    break;
                case HttpStatusCode.SwitchingProtocols:
                    break;
                case HttpStatusCode.Processing:
                    break;
                case HttpStatusCode.EarlyHints:
                    break;
                case HttpStatusCode.OK:
                    break;
                case HttpStatusCode.Created:
                    break;
                case HttpStatusCode.Accepted:
                    break;
                case HttpStatusCode.NonAuthoritativeInformation:
                    break;
                case HttpStatusCode.NoContent:
                    break;
                case HttpStatusCode.ResetContent:
                    break;
                case HttpStatusCode.PartialContent:
                    break;
                case HttpStatusCode.MultiStatus:
                    break;
                case HttpStatusCode.AlreadyReported:
                    break;
                case HttpStatusCode.IMUsed:
                    break;
                case HttpStatusCode.Ambiguous:
                    break;
                case HttpStatusCode.Moved:
                    break;
                case HttpStatusCode.Found:
                    break;
                case HttpStatusCode.RedirectMethod:
                    break;
                case HttpStatusCode.NotModified:
                    break;
                case HttpStatusCode.UseProxy:
                    break;
                case HttpStatusCode.Unused:
                    break;
                case HttpStatusCode.RedirectKeepVerb:
                    break;
                case HttpStatusCode.PermanentRedirect:
                    break;
                case HttpStatusCode.PaymentRequired:
                    break;
                case HttpStatusCode.MethodNotAllowed:
                    break;
                case HttpStatusCode.NotAcceptable:
                    break;
                case HttpStatusCode.ProxyAuthenticationRequired:
                    break;
                case HttpStatusCode.RequestTimeout:
                    break;
                case HttpStatusCode.Conflict:
                    break;
                case HttpStatusCode.Gone:
                    break;
                case HttpStatusCode.LengthRequired:
                    break;
                case HttpStatusCode.PreconditionFailed:
                    break;
                case HttpStatusCode.RequestEntityTooLarge:
                    break;
                case HttpStatusCode.RequestUriTooLong:
                    break;
                case HttpStatusCode.UnsupportedMediaType:
                    break;
                case HttpStatusCode.RequestedRangeNotSatisfiable:
                    break;
                case HttpStatusCode.ExpectationFailed:
                    break;
                case HttpStatusCode.MisdirectedRequest:
                    break;
                case HttpStatusCode.UnprocessableEntity:
                    break;
                case HttpStatusCode.Locked:
                    break;
                case HttpStatusCode.FailedDependency:
                    break;
                case HttpStatusCode.UpgradeRequired:
                    break;
                case HttpStatusCode.PreconditionRequired:
                    break;
                case HttpStatusCode.TooManyRequests:
                    break;
                case HttpStatusCode.RequestHeaderFieldsTooLarge:
                    break;
                case HttpStatusCode.UnavailableForLegalReasons:
                    break;
                case HttpStatusCode.NotImplemented:
                    break;
                case HttpStatusCode.BadGateway:
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    break;
                case HttpStatusCode.GatewayTimeout:
                    break;
                case HttpStatusCode.HttpVersionNotSupported:
                    break;
                case HttpStatusCode.VariantAlsoNegotiates:
                    break;
                case HttpStatusCode.InsufficientStorage:
                    break;
                case HttpStatusCode.LoopDetected:
                    break;
                case HttpStatusCode.NotExtended:
                    break;
                case HttpStatusCode.NetworkAuthenticationRequired:
                    break;
                default:
                    Stream receiveStream = post.Content.ReadAsStream();
                    StreamReader readStream = new(receiveStream, Encoding.UTF8);
                    var mensaje = readStream.ReadToEnd();
                    _Toast.ShowWarning("Error " + post.StatusCode + ". " + mensaje);
                    break;
            }
        }
        #endregion


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
                        var post = await Http.PostAsJsonAsync("", model);
                    });
                }
                catch (Exception)
                {

                }
            }
        }

    }
}
