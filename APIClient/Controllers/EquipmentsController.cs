using APIClient.LocalClass;
using APIClient.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonModels.Enums;
using CommonModels.Request;

namespace APIClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IEncrypterService _encrypterService;

        public EquipmentsController(IEncrypterService encrypterService)
        {
            _encrypterService = encrypterService;
        }



        //[HttpPost("Index")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public IActionResult Index([FromBody] Filter_Request Filters)
        //{
        //    #region Authorized
        //    if (User == null)
        //        return Unauthorized();
        //    if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Equipments, SystemActionsEnum.Read))
        //        return Forbid();
        //    #endregion Authorized

        //    try
        //    {
        //        return Ok(EquipmentsClass.CompleteInformation(User, Filters));
        //    }
        //    catch (Exception ex)
        //    {
        //        var result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //        return result;
        //    }
        //}        


        [HttpPost("EquipmentsOfStation")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult EquipmentsOfStation([FromBody] long IDstation)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Equipments, SystemActionsEnum.Read))
                return Forbid();
            #endregion Authorized

            try
            {
                return Ok(EquipmentsClass.ListOfEquipments(User, IDstation));
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                return result;
            }
        }

        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create([FromBody] Equipment_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Equipments, SystemActionsEnum.Create))
                return Forbid();
            #endregion Authorized

            try
            {
                string URL;

                if (HttpContext.Request.IsHttps)
                    URL = "https://" + HttpContext.Request.Host.Value + "/";
                else
                    URL = "http://" + HttpContext.Request.Host.Value + "/";

                var _Respuesta = EquipmentsClass.Create(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(new LocalResponse_Request(ClientsClass.SearchClient(User), StationsClass.SearchStation(User, URL), UsersClass.SearchUser(User))
                    { ObjetoJson = _Respuesta.Mensaje });

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("Modify")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Modify([FromBody] Equipment_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Equipments, SystemActionsEnum.Modify))
                return Forbid();
            #endregion Authorized

            try
            {

                string URL;

                if (HttpContext.Request.IsHttps)
                    URL = "https://" + HttpContext.Request.Host.Value + "/";
                else
                    URL = "http://" + HttpContext.Request.Host.Value + "/";


                var _Respuesta = EquipmentsClass.Modify(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(new LocalResponse_Request(ClientsClass.SearchClient(User), StationsClass.SearchStation(User, URL), UsersClass.SearchUser(User))
                    { ObjetoJson = _Respuesta.Mensaje });

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpPost("Delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete([FromBody] DeleteEquipment_Request model)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Equipments, SystemActionsEnum.Delete))
                return Forbid();
            #endregion Authorized

            try
            {
                string URL;

                if (HttpContext.Request.IsHttps)
                    URL = "https://" + HttpContext.Request.Host.Value + "/";
                else
                    URL = "http://" + HttpContext.Request.Host.Value + "/";


                var _Respuesta = EquipmentsClass.Delete(User, model);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                    return Ok(new LocalResponse_Request(ClientsClass.SearchClient(User), StationsClass.SearchStation(User, URL), UsersClass.SearchUser(User))
                    { ObjetoJson = _Respuesta.Mensaje });

                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
