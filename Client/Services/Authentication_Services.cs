using Client.LocalClass;
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

namespace Client.Services
{
    public class Authentication_Services : IAuthentication_Services
    {
        [Inject]
        private HttpClient _HttpClient { get; set; }

        private readonly IGlobalElements_Services _GlobalElements;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorage_Services _LocalStorage;
        private readonly IURLs_Services _URLs;

        public Authentication_Services(HttpClient client, AuthenticationStateProvider authStateProvider, IGlobalElements_Services globalElements, ILocalStorage_Services localStorage, IURLs_Services uRLs)
        {
            _GlobalElements = globalElements;
            _HttpClient = client;
            _authStateProvider = authStateProvider;
            _LocalStorage = localStorage;
            _URLs = uRLs;
        }


        public async Task<string> Login(Login_Request userForAuthentication, IToast_Services _Toast)
        {
            return await _Login(userForAuthentication, _Toast, await _URLs.Login());
        }

        public async Task<string> SignUp(Login_Request userForAuthentication, IToast_Services _Toast)
        {
            return await _Login(userForAuthentication, _Toast, await _URLs.SignUp());
        }
        private async Task<string> _Login(Login_Request userForAuthentication, IToast_Services _Toast, string url)
        {
            HttpResponseMessage post = null;

            try
            {
                var content = JsonConvert.SerializeObject(userForAuthentication);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
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

                    if (response == null || !response.IsAuthSuccessful)
                        return "false";

                    await _LocalStorage.SetLogin(response);

                    _GlobalElements.User = await _LocalStorage.GetDecodified_Login();
                    _GlobalElements.TitleOfPage = _GlobalElements.Client.Name;
                    _GlobalElements.Client = _GlobalElements.Client;

                    ((AuthenticationStateProvider_Services)_authStateProvider).NotifyUserAuthentication(userForAuthentication.Email);
                    _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);

                    return string.Empty;
                }
                else
                {
                    if (post != null)
                    {
                        switch (post.StatusCode)
                        {
                            case HttpStatusCode.BadRequest:
                                _Toast.ShowError("Email or password invalid.");
                                break;
                        }
                    }
                    else
                    {
                        _Toast.ShowError("Could not connect to server.");
                    }
                    return "false";
                }
            }
            catch (Exception e)
            {
                HttpClass.NuevoLog(_HttpClient, "Could not deserialize.", SystemActionsEnum.Login, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);

                _Toast.ShowError("Could not authenticate.");
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

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, await _URLs.Login_RestorePassword())
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
                HttpClass.NuevoLog(_HttpClient, "Could not restore password. Could not deserialize.", SystemActionsEnum.Login, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);

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

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, await _URLs.Login_UpdatePassword())
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
                HttpClass.NuevoLog(_HttpClient, "Could not update password. Could not deserialize.", SystemActionsEnum.Login, SystemTypesEnum.WEB, e, SystemErrorCodesEnum.Error);

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
