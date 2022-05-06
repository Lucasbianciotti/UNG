using API.LocalClass;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Models.Request;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IEncrypterService _encrypterService;

        public DataController(IEncrypterService encrypterService)
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
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Data, SystemActionsEnum.Read))
                return Forbid();
            #endregion Authorized

            try
            {
                return Ok(DataClass.CompleteInformation(User, Filters));
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                return result;
            }
        }

        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create([FromBody] Data_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Data, SystemActionsEnum.Create))
                return Forbid();
            #endregion Authorized

            try
            {
                var _Respuesta = DataClass.Create(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(DataClass.PartialInformation(User, model.Filters));

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
