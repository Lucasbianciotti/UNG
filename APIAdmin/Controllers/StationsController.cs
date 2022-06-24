using APIAdmin.LocalClass;
using APIAdmin.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Models.Request;

namespace APIAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly IEncrypterService _encrypterService;

        public StationsController(IEncrypterService encrypterService)
        {
            _encrypterService = encrypterService;
        }



        [HttpPost("Index")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Index([FromBody] Filter_Request Filters)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Stations, SystemActionsEnum.Read))
                return Forbid();
            #endregion Authorized

            try
            {
                return Ok(StationsClass.CompleteInformation(User, Filters));
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                return result;
            }
        }

        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create([FromBody] Station_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Stations, SystemActionsEnum.Create))
                return Forbid();
            #endregion Authorized

            try
            {
                var _Respuesta = StationsClass.Create(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(StationsClass.PartialInformation(User, model.Filters));

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Modify")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Modify([FromBody] Station_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Stations, SystemActionsEnum.Modify))
                return Forbid();
            #endregion Authorized

            try
            {
                var _Respuesta = StationsClass.Modify(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(StationsClass.PartialInformation(User, model.Filters));

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }



        [HttpPost("Delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete([FromBody] DeleteStation_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Stations, SystemActionsEnum.Delete))
                return Forbid();
            #endregion Authorized

            try
            {
                var _Respuesta = StationsClass.Delete(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(StationsClass.PartialInformation(User, model.Filters));

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
