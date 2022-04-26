using API.LocalClass;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.EntityFrameworks;
using Models.Enums;
using Models.Request;

namespace API.Controllers
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
                return BadRequest("Modelo no válido.");


            using var db = new UNG_Context();
            var usuarioAdmin = UsersClass.LoginDeUsuario(db, model.Email, model.Password);
            if (usuarioAdmin == null) return BadRequest("Usuario o contraseña incorrecta");


            var respuesta = _userService.Login(usuarioAdmin);
            if (respuesta == null)
                return BadRequest("Usuario o contraseña incorrecta");

            return Ok(respuesta);
        }


        [HttpPost("Login_ReestablecerContrasena")]
        public IActionResult Login_ReestablecerContrasena([FromBody] Login_ReestablecerContraseña_Request model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo no válido.");


            var response = _userService.ReestablecerContraseña(model);
            if (!response)
                return BadRequest("No se pudo reestablecer la contraseña. Reintente.");

            return Ok("Si el email fue correcto, se le envió un correo para reestablecer su contraseña.");
        }


        [HttpPost("Login_ActualizarContrasena")]
        public IActionResult Login_ActualizarContrasena([FromBody] Login_ActualizarContraseña_Request model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo no válido.");


            var response = _userService.ActualizarContraseña(model);
            if (response)
                return BadRequest("No se pudo actualizar la contraseña. Reintente.");

            return Ok("Contraseña actualizada con éxito. Ahora puede iniciar sesión.");
        }








        [HttpGet("Index")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Index()
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UsuarioTienePermiso(User, SeccionesEnum.Users, AccionesEnum.View))
                return Forbid();
            #endregion Authorized

            try
            {
                return Ok(UsersClass.InformacionCompleta(User));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("Crear")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Crear([FromBody] User_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UsuarioTienePermiso(User, SeccionesEnum.Users, AccionesEnum.Create))
                return Forbid();
            #endregion Authorized

            try
            {
                var _Respuesta = UsersClass.Crear(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(UsersClass.InformacionParcial(User));

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Modificar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Modificar([FromBody] User_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UsuarioTienePermiso(User, SeccionesEnum.Users, AccionesEnum.Modify))
                return Forbid();
            #endregion Authorized

            try
            {
                var _Respuesta = UsersClass.Modificar(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(UsersClass.InformacionParcial(User));

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpPost("CambiarContrasena")]
        public IActionResult CambiarContrasena([FromBody] CambiarContraseñaUsuario_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            //if (!UsersClass.UsuarioTienePermiso(User, SeccionesEnum.Users, AccionesEnum.Modificar))
            //    return Forbid();
            #endregion Authorized

            try
            {
                var _Respuesta = UsersClass.CambiarContraseña(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(new GlobalResponse_Request());

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("Eliminar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Eliminar([FromBody] EliminarUsuario_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UsuarioTienePermiso(User, SeccionesEnum.Users, AccionesEnum.Detele))
                return Forbid();
            #endregion Authorized

            try
            {
                var _Respuesta = UsersClass.Eliminar(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(UsersClass.InformacionParcial(User));

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
