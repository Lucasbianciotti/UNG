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
    public class DashboardController : ControllerBase
    {
        private readonly IEncrypterService _encrypterService;

        public DashboardController(IEncrypterService encrypterService)
        {
            _encrypterService = encrypterService;
        }



        [HttpGet("Index")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Index()
        {
            #region Authorized
            if (User == null)
                return Unauthorized();
            if (!UsersClass.UserHasPermission(User, SystemSectionsEnum.Dashboard, SystemActionsEnum.Read))
                return Forbid();
            #endregion Authorized

            try
            {
                string URL;

                if (HttpContext.Request.IsHttps)
                    URL = "https://" + HttpContext.Request.Host.Value + "/";
                else
                    URL = "http://" + HttpContext.Request.Host.Value + "/";

                return Ok(StationsClass.CompleteInformation(User, URL));
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                return result;
            }
        }


    }
}
