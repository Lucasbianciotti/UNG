using APIClient.LocalClass;
using APIClient.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Models.Request;

namespace APIClient.Controllers
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
        public IActionResult Index([FromBody] FilterData_Request Filters)
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Data, SystemActionsEnum.Read))
                return Forbid();
            #endregion Authorized

            try
            {
                string URL;

                if (HttpContext.Request.IsHttps)
                    URL = "https://" + HttpContext.Request.Host.Value + "/";
                else
                    URL = "http://" + HttpContext.Request.Host.Value + "/";


                return Ok(DataClass.CompleteInformation(User, Filters, URL));
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                return result;
            }
        }


        [HttpGet("Delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete()
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Data, SystemActionsEnum.Delete))
                return Forbid();
            #endregion Authorized

            try
            {
                return Ok(DataClass.Delete(User));
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                return result;
            }
        }


        [HttpGet("RecreateTables")]
        public IActionResult RecreateTables()
        {
            try
            {
                return Ok(DataClass.RecreateTables(User));
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                return result;
            }
        }

    }
}
