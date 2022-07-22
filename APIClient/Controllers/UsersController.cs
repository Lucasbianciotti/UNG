using APIClient.LocalClass;
using APIClient.LocalModels.SQLite;
using APIClient.Services;
using Class;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonModels.Enums;
using CommonModels.Request;
using Newtonsoft.Json;

namespace APIClient.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login_Request model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model not valid.");


            using var db = new Local_Context();

            string passwordEncrypted = EncrypterService.GetSHA256(model.Password);
            string emailEncrypted = EncodifierClass.Codify(model.Email.ToLower().Trim());
            string estadoEncrypted = EncodifierClass.Codify(UsersStatusEnum.Active);

            var userAdmin = db.Users.Where(x => x.Email == emailEncrypted && x.PasswordHash == passwordEncrypted && x.IDstatus == estadoEncrypted).FirstOrDefault();
            if (userAdmin == null) return BadRequest("User or password incorrect");



            string URL;

            if (HttpContext.Request.IsHttps)
                URL = "https://" + HttpContext.Request.Host.Value + "/";
            else
                URL = "http://" + HttpContext.Request.Host.Value + "/";

            var respuesta = _userService.Login(userAdmin, URL);
            if (respuesta == null) return BadRequest("User or password incorrect");

            return Ok(respuesta);
        }


        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] Login_Request model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model not valid.");


            using var db = new Local_Context();

            string passwordEncrypted = EncrypterService.GetSHA256(model.Password);
            string emailEncrypted = EncodifierClass.Codify(model.Email.ToLower().Trim());
            string estadoEncrypted = EncodifierClass.Codify(UsersStatusEnum.Active);

            var listPermission = new List<Permissions_Request>() {
                new Permissions_Request()
                {
                    IDsection =(int)SystemSectionsEnum.Dashboard,
                    Read=true,
                    Create=true,
                    Delete=true,
                    Export=true,
                    Modify=true,
                },
                new Permissions_Request()
                {
                    IDsection =(int)SystemSectionsEnum.Data,
                    Read=true,
                    Create=true,
                    Delete=true,
                    Export=true,
                    Modify=true,
                },
                new Permissions_Request()
                {
                    IDsection =(int)SystemSectionsEnum.Configuration,
                    Read=true,
                    Create=true,
                    Delete=true,
                    Export=true,
                    Modify=true,
                },
                new Permissions_Request()
                {
                    IDsection =(int)SystemSectionsEnum.Users,
                    Read=true,
                    Create=true,
                    Delete=true,
                    Export=true,
                    Modify=true,
                },

            };

            try
            {
                db.Database.EnsureCreated();

                var Data = new Users
                {
                    Email = emailEncrypted,
                    PasswordHash = passwordEncrypted,
                    IDstatus = estadoEncrypted,
                    DateOf_Creation = EncodifierClass.Codify(DateTime.Now.ToString()),
                    DateOf_LastLogin = EncodifierClass.Codify(DateTime.Now.ToString()),
                    Name = EncodifierClass.Codify("User"),
                    Surname = EncodifierClass.Codify("Lastname"),
                    IDrole = 2,
                    IDung = 1,
                    JSONListOfPermissions = EncodifierClass.Codify(JsonConvert.SerializeObject(listPermission)),
                    PinRestorePassword="1",
                };

                db.Users.Add(Data);

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }


            var userAdmin = db.Users.Where(x => x.Email == emailEncrypted && x.PasswordHash == passwordEncrypted && x.IDstatus == estadoEncrypted).FirstOrDefault();
            if (userAdmin == null) return BadRequest("User or password incorrect");



            string URL;

            if (HttpContext.Request.IsHttps)
                URL = "https://" + HttpContext.Request.Host.Value + "/";
            else
                URL = "http://" + HttpContext.Request.Host.Value + "/";

            var respuesta = _userService.Login(userAdmin, URL);
            if (respuesta == null) return BadRequest("User or password incorrect");

            return Ok(respuesta);
        }


        [HttpPost("Login_RestorePassword")]
        public IActionResult Login_ReestablecerContrasena([FromBody] Login_RestorePassword_Request model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model not valid.");


            var response = _userService.RestorePassword(model);
            if (!response)
                return BadRequest("Could not restore password. Retry.");

            return Ok("Si el email fue correcto, se le envió un correo para reestablecer su password.");
        }


        [HttpPost("Login_UpdatePassword")]
        public IActionResult Login_UpdatePassword([FromBody] Login_UpdatePassword_Request model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo no válido.");


            var response = _userService.UpdatePassword(model);
            if (response)
                return BadRequest("Could not update password. Retry.");

            return Ok("Password updated successfully. Login.");
        }





        [HttpPost("Index")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Index([FromBody] Filter_Request Filters)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Users, SystemActionsEnum.Read))
                return Forbid();
            #endregion Authorized

            try
            {
                string URL;

                if (HttpContext.Request.IsHttps)
                    URL = "https://" + HttpContext.Request.Host.Value + "/";
                else
                    URL = "http://" + HttpContext.Request.Host.Value + "/";


                return Ok(UsersClass.CompleteInformation(User, Filters, URL));
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                return result;
            }
        }


        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create([FromBody] User_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Users, SystemActionsEnum.Create))
                return Forbid();
            #endregion Authorized

            try
            {
                string URL;

                if (HttpContext.Request.IsHttps)
                    URL = "https://" + HttpContext.Request.Host.Value + "/";
                else
                    URL = "http://" + HttpContext.Request.Host.Value + "/";


                var _Respuesta = UsersClass.Create(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(UsersClass.CompleteInformation(User, new Filter_Request(), URL));

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Modify")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Modify([FromBody] User_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Users, SystemActionsEnum.Modify))
                return Forbid();
            #endregion Authorized

            try
            {
                string URL;

                if (HttpContext.Request.IsHttps)
                    URL = "https://" + HttpContext.Request.Host.Value + "/";
                else
                    URL = "http://" + HttpContext.Request.Host.Value + "/";


                var _Respuesta = UsersClass.Modify(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(UsersClass.CompleteInformation(User, new Filter_Request(), URL));

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordUser_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            //if (!UsersClass.UserTienePermiso(User, SeccionesEnum.Users, AccionesEnum.Modify))
            //    return Forbid();
            #endregion Authorized

            try
            {
                string URL;

                if (HttpContext.Request.IsHttps)
                    URL = "https://" + HttpContext.Request.Host.Value + "/";
                else
                    URL = "http://" + HttpContext.Request.Host.Value + "/";



                var _Respuesta = UsersClass.ChangePassword(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(new LocalResponse_Request(ClientsClass.SearchClient(User), StationsClass.SearchStation(User, URL), UsersClass.SearchUser(User)));

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("Delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete([FromBody] DeleteUser_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Users, SystemActionsEnum.Delete))
                return Forbid();
            #endregion Authorized

            try
            {
                string URL;

                if (HttpContext.Request.IsHttps)
                    URL = "https://" + HttpContext.Request.Host.Value + "/";
                else
                    URL = "http://" + HttpContext.Request.Host.Value + "/";


                var _Respuesta = UsersClass.Delete(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(UsersClass.CompleteInformation(User, model.Filters, URL));

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
