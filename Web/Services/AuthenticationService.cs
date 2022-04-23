using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Models;
using Models.Enums;
using Models.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Web.LocalClass;

namespace Web.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        [Inject]
        private HttpClient _HttpClient { get; set; }

        private readonly IGlobalElements_Services _GlobalElements;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage, IGlobalElements_Services globalElements)
        {
            _GlobalElements = globalElements;
            _HttpClient = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }


        public async Task<string> GetLogin_CompanyName()
        {
            try
            {
                if (await _localStorage.GetItemAsync<long>("ID") == 0)
                    return string.Empty;

                return await _localStorage.GetItemAsync<string>("Empresa");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<Usuario_Request> GetLogin()
        {
            try
            {
                if (await _localStorage.GetItemAsync<long>("ID") == 0)
                    return null;

                var usuario = new Usuario_Request
                {
                    ID = await _localStorage.GetItemAsync<long>("ID"),
                    Nombre = await _localStorage.GetItemAsync<string>("Nombre"),
                    Apellido = await _localStorage.GetItemAsync<string>("Apellido"),
                    Email = await _localStorage.GetItemAsync<string>("Email"),
                    URL_ImagenDePerfil = await _localStorage.GetItemAsync<string>("URL_ImagenDePerfil"),
                    PermisosDeUsuario = await _localStorage.GetItemAsync<List<PermisosDeUsuario_Request>>("PermisosDeUsuario"),
                    IDempresa = await _localStorage.GetItemAsync<long>("IDempresa"),
                    Empresa = await _localStorage.GetItemAsync<string>("Empresa")
                };

                usuario.NombreCompleto = usuario.Nombre + " " + usuario.Apellido;

                _GlobalElements.TituloDePagina = "UNG system - " + usuario.Empresa;
                _GlobalElements.Empresa = usuario.Empresa;

                return usuario;
            }
            catch (Exception)
            {
                return null;
            }
        }



        public async Task<string> Login(Login_Request userForAuthentication)
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

                    await _localStorage.SetItemAsync("authToken", response.Token);
                    await _localStorage.SetItemAsync("ID", response.ID);
                    await _localStorage.SetItemAsync("Nombre", response.Nombre);
                    await _localStorage.SetItemAsync("Apellido", response.Apellido);
                    await _localStorage.SetItemAsync("Email", response.Email);
                    await _localStorage.SetItemAsync("URL_ImagenDePerfil", response.URL_ImagenDePerfil);
                    await _localStorage.SetItemAsync("PermisosDeUsuario", response.PermisosDeUsuario);
                    await _localStorage.SetItemAsync("IDempresa", response.IDempresa);
                    await _localStorage.SetItemAsync("Empresa", response.Empresa);

                    _GlobalElements.TituloDePagina = "UNG system - " + response.Empresa;
                    _GlobalElements.Empresa = response.Empresa;

                    ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthentication.Email);
                    _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);

                    return string.Empty;
                }
                else
                {
                    return HttpClass.TypeError(post);
                }
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(_HttpClient, "No se pudo deserializar.", AccionesDelSistemaEnum.Login, TiposDeSistemaEnum.WEB, e, CodigosDeErrorEnum.Error);

                return "No se pudo autenticar. Verifique su conexión a internet.";
            }
        }

        public async Task<string> ReestablecerContraseña(Login_ReestablecerContraseña_Request Model)
        {
            HttpResponseMessage post = null;

            try
            {
                var content = JsonConvert.SerializeObject(Model);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, URLs.Login_ReestablecerContraseña)
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
                    return HttpClass.TypeError(post);
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(_HttpClient, "No se pudo reestablecer la contraseña. No se pudo deserializar.", AccionesDelSistemaEnum.Login, TiposDeSistemaEnum.WEB, e, CodigosDeErrorEnum.Error);

                return "No se pudo reestablecer la contraseña. Verifique su conexión a internet.";
            }

        }

        public async Task<string> ActualizarContraseña(Login_ActualizarContraseña_Request Model)
        {
            HttpResponseMessage post = null;

            try
            {
                var content = JsonConvert.SerializeObject(Model);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, URLs.Login_ActualizarContraseña)
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
                    return HttpClass.TypeError(post);
            }
            catch (Exception e)
            {
                Logs_ErroresClass.NuevoLog(_HttpClient, "No se pudo actualizar la contraseña. No se pudo deserializar.", AccionesDelSistemaEnum.Login, TiposDeSistemaEnum.WEB, e, CodigosDeErrorEnum.Error);

                return "No se pudo actualizar la contraseña. Verifique su conexión a internet.";
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("ID");
            await _localStorage.RemoveItemAsync("Nombre");
            await _localStorage.RemoveItemAsync("Apellido");
            await _localStorage.RemoveItemAsync("Email");
            await _localStorage.RemoveItemAsync("URL_ImagenDePerfil");
            await _localStorage.RemoveItemAsync("PermisosDeUsuario");
            await _localStorage.RemoveItemAsync("Empresa");
            await _localStorage.RemoveItemAsync("IDempresa");

            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _HttpClient.DefaultRequestHeaders.Authorization = null;
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
