using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Models;
using Models.Enums;
using Models.Request;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Web.LocalClass;

namespace Web.Services
{
    public class Authentication_Services : IAuthentication_Services
    {
        [Inject]
        private HttpClient _HttpClient { get; set; }

        private readonly IGlobalElements_Services _GlobalElements;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorage_Services _LocalStorage;

        public Authentication_Services(HttpClient client, AuthenticationStateProvider authStateProvider, IGlobalElements_Services globalElements, ILocalStorage_Services localStorage)
        {
            _GlobalElements = globalElements;
            _HttpClient = client;
            _authStateProvider = authStateProvider;
            _LocalStorage = localStorage;
        }






        public async Task<string> Login(Login_Request userForAuthentication, IToast_Services _Toast)
        {
            HttpResponseMessage post = null;

            try
            {
                var content = JsonConvert.SerializeObject(userForAuthentication);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, URLs.Login)
                {
                    Content = bodyContent
                };
                requestMessage.SetBrowserRequestMode(BrowserRequestMode.Cors);

                post = await _HttpClient.SendAsync(requestMessage);
            }
            catch (Exception)
            { }

            try
            {
                if (post != null && post.IsSuccessStatusCode)
                {
                    var cadena = await post.Content.ReadAsStringAsync();

                    var response = JsonConvert.DeserializeObject<Response_Login_Request>(cadena);

                    await _LocalStorage.SetLogin(response.User);

                    _GlobalElements.User = await _LocalStorage.GetDecodified_Login();
                    _GlobalElements.TitleOfPage = "UNG system - " + _GlobalElements.User.Client;
                    _GlobalElements.Client = _GlobalElements.User.Client;

                    ((AuthenticationStateProvider_Services)_authStateProvider).NotifyUserAuthentication(userForAuthentication.Email);
                    _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);

                    return string.Empty;
                }
                else
                {
                    switch (post.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            _Toast.ShowError("Email or password invalid.");
                            break;
                    }
                    return "false";
                }
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(_HttpClient, "Could not deserialize.", SystemActionsEnum.Login, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);

                return "Could not authenticate.";
            }
        }

        public async Task<string> RestorePassword(Login_RestorePassword_Request Model, IToast_Services _Toast)
        {
            HttpResponseMessage post = null;

            try
            {
                var content = JsonConvert.SerializeObject(Model);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, URLs.Login_ReestablecerPassword)
                {
                    Content = bodyContent
                };
                requestMessage.SetBrowserRequestMode(BrowserRequestMode.Cors);

                post = await _HttpClient.SendAsync(requestMessage);
            }
            catch (Exception)
            { }

            try
            {
                if (post != null && post.IsSuccessStatusCode)
                    return string.Empty;
                else
                {
                    HttpClass.TypeError(_Toast, post);
                    return "false";
                }
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(_HttpClient, "Could not restore password. Could not deserialize.", SystemActionsEnum.Login, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);

                return "Could not restore password.";
            }

        }

        public async Task<string> UpdatePassword(Login_UpdatePassword_Request Model, IToast_Services _Toast)
        {
            HttpResponseMessage post = null;

            try
            {
                var content = JsonConvert.SerializeObject(Model);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, URLs.Login_ActualizarPassword)
                {
                    Content = bodyContent
                };
                requestMessage.SetBrowserRequestMode(BrowserRequestMode.Cors);

                post = await _HttpClient.SendAsync(requestMessage);
            }
            catch (Exception)
            { }

            try
            {
                if (post != null && post.IsSuccessStatusCode)
                    return string.Empty;
                else
                {
                    HttpClass.TypeError(_Toast, post);
                    return "false";
                }
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(_HttpClient, "Could not update password. Could not deserialize.", SystemActionsEnum.Login, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);

                return "Could not update password.";
            }
        }

        public async Task Logout()
        {
            try
            {
                await _LocalStorage.RemoveLogin();

                ((AuthenticationStateProvider_Services)_authStateProvider).NotifyUserLogout();
                _HttpClient.DefaultRequestHeaders.Authorization = null;
            }
            catch (Exception e)
            {

            }
        }



        //public async Task<RegistrationResponseDto> RegisterUser(Login_ViewModel userForRegistration)
        //{
        //    var content = JsonSerializer.Serialize(userForRegistration);
        //    var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        //    var registrationResult = await _client.PostAsync("https://localhost:5011/api/accounts/registration", bodyContent);
        //    var registrationContent = await registrationResult.Content.ReadAsStringAsync();

        //    if (!registrationResult.IsSuccessStatusCode)
        //    {
        //        var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //        return result;
        //    }

        //    return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        //}


    }

}
