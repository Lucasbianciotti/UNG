using Models;
using Models.Enums;
using Models.Request;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Web.Services;

namespace Web.LocalClass
{
    public static class HttpClass
    {

        public static async Task<Dashboard_Request> Get_Dashboard(HttpClient Http, FilterDashboard_Request filtros, IToastServices _Toast, IGlobalElements_Services _GlobalElements)
        {
            try
            {
                HttpResponseMessage post = await Post(Http, URLs.Dashboard, filtros, _Toast);

                if (post != null && post.IsSuccessStatusCode)
                {
                    var cadena = await post.Content.ReadAsStringAsync();

                    var response = JsonConvert.DeserializeObject<Dashboard_Request>(cadena);

                    return response;
                }
                else
                {
                    TypeError(_Toast, post);
                }
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(Http, "No se pudo deserializar.", AccionesDelSistemaEnum.GET, TiposDeSistemaEnum.WEB, e, CodigosDeErrorEnum.Error);

                _Toast.ShowError("The information could not be loaded.");
            }

            return null;
        }


        #region Get
        public static async Task<bool> GetSet(HttpClient Http, string URL, IToastServices _Toast, IGlobalElements_Services _GlobalElements, Filter_Request filtros = null)
        {
            try
            {
                GlobalResponse_Request response = null;
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
                Logs_ErroresClass.NuevoLog(Http, "No se pudo deserializar.", AccionesDelSistemaEnum.GET, TiposDeSistemaEnum.WEB, e, CodigosDeErrorEnum.Error);
                _Toast.ShowError("The information could not be loaded.");
            }

            return false;
        }

        public static async Task<GlobalResponse_Request> GetOnly(HttpClient Http, string URL, IToastServices _Toast, Filter_Request filtros = null, long? ID = null)
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
                        return JsonConvert.DeserializeObject<GlobalResponse_Request>(cadena);
                    }
                    else
                    {
                        TypeError(_Toast, post);
                    }
                }
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(Http, "No se pudo deserializar.", AccionesDelSistemaEnum.GET, TiposDeSistemaEnum.WEB, e, CodigosDeErrorEnum.Error);
                _Toast.ShowError("The information could not be loaded.");
            }

            return null;
        }
        #endregion Get


        #region Post
        public static async Task<bool> PostSet(HttpClient Http, string URL, object model, IToastServices _Toast, IGlobalElements_Services _GlobalElements)
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
                Logs_ErroresClass.NuevoLog(Http, "No se pudo deserializar.", AccionesDelSistemaEnum.POST, TiposDeSistemaEnum.WEB, e, CodigosDeErrorEnum.Error);
                _Toast.ShowError("The information could not be loaded.");
            }

            return false;
        }

        public static async Task<GlobalResponse_Request> PostOnly(HttpClient Http, string URL, object model, IToastServices _Toast)
        {
            try
            {
                HttpResponseMessage post = await Post(Http, URL, model, _Toast);

                if (post != null && post.IsSuccessStatusCode)
                {
                    var cadena = await post.Content.ReadAsStringAsync();
                    var temp = JsonConvert.DeserializeObject<GlobalResponse_Request>(cadena);

                    if (!string.IsNullOrEmpty(temp._Message))
                        _Toast.ShowSuccess(temp._Message, "Atención");

                    return temp;
                }

                TypeError(_Toast, post);
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(Http, "No se pudo deserializar.", AccionesDelSistemaEnum.POST, TiposDeSistemaEnum.WEB, e, CodigosDeErrorEnum.Error);
                _Toast.ShowError("The information could not be loaded.");
            }

            return null;
        }
        #endregion Post




        public static string TypeError(HttpResponseMessage post)
        {
            if (string.IsNullOrEmpty(post.RequestMessage.ToString()))
            {
                switch (post.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        return "Por favor inicie sesión.";

                    case HttpStatusCode.Forbidden:
                        return "No tiene los permisos necesarios.";

                    case HttpStatusCode.BadRequest:
                        return "No se pudo conectar con el servidor.";

                    case HttpStatusCode.NotFound:
                        return "The URL was not found.";

                    case HttpStatusCode.InternalServerError:
                        return "Error en el servidor.";
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
                        return "Error " + post.StatusCode;
                }

                return "Error " + post.StatusCode;
            }
            else
            {
                return "Error " + post.StatusCode + ".\n" + post.RequestMessage;
            }
        }



        #region Private methods
        private static async Task<HttpResponseMessage> Post(HttpClient Http, string URL, object model, IToastServices _Toast)
        {
            try
            {
                //var strings = JsonConvert.SerializeObject(model);
                //string jsonString = System.Text.Json.JsonSerializer.Serialize(model);

                //var asd = strings.Length;
                //var asdd = jsonString.Length;
                return await Http.PostAsJsonAsync(URL, model);
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(Http, "No se pudo conectar con el servidor.", AccionesDelSistemaEnum.POST, TiposDeSistemaEnum.WEB, e, CodigosDeErrorEnum.Error);
                _Toast.ShowError("No se pudo conectar con el servidor. Verifique su conexión a internet.");
                return null;
            }
        }
        private static async Task<HttpResponseMessage> Get(HttpClient Http, string URL, IToastServices _Toast)
        {
            try
            {
                return await Http.GetAsync(URL);
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(Http, "No se pudo conectar con el servidor.", AccionesDelSistemaEnum.GET, TiposDeSistemaEnum.WEB, e, CodigosDeErrorEnum.Error);
                _Toast.ShowError("No se pudo conectar con el servidor. Verifique su conexión a internet.");
                return null;
            }
        }

        private static void TypeError(IToastServices _Toast, HttpResponseMessage post)
        {
            Stream receiveStream = post.Content.ReadAsStream();
            StreamReader readStream = new(receiveStream, Encoding.UTF8);
            var mensaje = readStream.ReadToEnd();

            switch (post.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _Toast.ShowWarning("Por favor inicie sesión.");
                    break;

                case HttpStatusCode.Forbidden:
                    _Toast.ShowWarning("No tiene los permisos necesarios.");
                    break;

                case HttpStatusCode.BadRequest:
                    _Toast.ShowError("No se pudo conectar con el servidor.");
                    break;

                case HttpStatusCode.NotFound:
                    _Toast.ShowError("No se encontró la URL.");
                    break;

                //case HttpStatusCode.InternalServerError:
                //    _Toast.ShowError("Error en el servidor. Contacte al proveedor.");
                //    break;

                default:
                    _Toast.ShowWarning(mensaje);
                    break;
            }
        }
        #endregion

    }
}
