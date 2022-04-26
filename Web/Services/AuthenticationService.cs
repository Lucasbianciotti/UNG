using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Models;
using Models.Enums;
using Models.Request;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
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

                return await _localStorage.GetItemAsync<string>("Company");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<User_Request> GetLogin()
        {
            try
            {
                if (await _localStorage.GetItemAsync<long>("ID") == 0)
                    return null;

                var usuario = new User_Request
                {
                    ID = await _localStorage.GetItemAsync<long>("ID"),
                    Name = await _localStorage.GetItemAsync<string>("Name"),
                    Surname = await _localStorage.GetItemAsync<string>("Surname"),
                    Email = await _localStorage.GetItemAsync<string>("Email"),
                    URL_ImagenDePerfil = await _localStorage.GetItemAsync<string>("URL_ImagenDePerfil"),
                    PermisosDeUsuario = await _localStorage.GetItemAsync<List<PermisosDeUsuario_Request>>("PermisosDeUsuario"),
                    IDcompany = await _localStorage.GetItemAsync<long>("IDcompany"),
                    Company = await _localStorage.GetItemAsync<string>("Company")
                };

                usuario.Name = EncryptClass.Decodify(usuario.Name);
                usuario.Surname= EncryptClass.Decodify(usuario.Surname);
                usuario.CompleteName = EncryptClass.Decodify(usuario.Name) + " " + EncryptClass.Decodify(usuario.Surname);
                usuario.Company = EncryptClass.Decodify(usuario.Company);

                _GlobalElements._Usuario = usuario;

                _GlobalElements.TituloDePagina = "UNG system - " + usuario.Company;
                _GlobalElements.Company = usuario.Company;

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
                    await _localStorage.SetItemAsync("Name", response.Name);
                    await _localStorage.SetItemAsync("Surname", response.Surname);
                    await _localStorage.SetItemAsync("Email", response.Email);
                    await _localStorage.SetItemAsync("URL_ImagenDePerfil", response.URL_ImagenDePerfil);
                    await _localStorage.SetItemAsync("PermisosDeUsuario", response.PermisosDeUsuario);
                    await _localStorage.SetItemAsync("IDcompany", response.IDcompany);
                    await _localStorage.SetItemAsync("Company", response.Company);

                    _GlobalElements.TituloDePagina = "UNG system - " + response.Company;
                    _GlobalElements.Company = response.Company;

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

        public async Task<string> ReestablecerContraseña(Login_RestorePassword_Request Model)
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

        public async Task<string> ActualizarContraseña(Login_UpdatePassword_Request Model)
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
            await _localStorage.RemoveItemAsync("Name");
            await _localStorage.RemoveItemAsync("Surname");
            await _localStorage.RemoveItemAsync("Email");
            await _localStorage.RemoveItemAsync("URL_ImagenDePerfil");
            await _localStorage.RemoveItemAsync("PermisosDeUsuario");
            await _localStorage.RemoveItemAsync("Company");
            await _localStorage.RemoveItemAsync("IDcompany");

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
