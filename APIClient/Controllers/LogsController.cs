using APIClient.LocalClass;
using APIClient.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using CommonModels.Enums;
using CommonModels.Request;

namespace APIClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly IHubContext<SignalRService> hubContext;

        public LogsController(IHubContext<SignalRService> surveyHub)
        {
            this.hubContext = surveyHub;
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
                string URL;

                if (HttpContext.Request.IsHttps)
                    URL = "https://" + HttpContext.Request.Host.Value + "/";
                else
                    URL = "http://" + HttpContext.Request.Host.Value + "/";


                return Ok(LogsClass.CompleteInformation(User, Filters, URL));
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                return result;
            }
        }




        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Disparo_Request model)
        {
            try
            {
                var guid = Guid.NewGuid().ToString();

                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(model.timestamp);
                DateTime datetime = new(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second);

                var data = new Data_Request()
                {
                    Timespan = model.timestamp,
                    Datetime = datetime,
                    Info = model.data,
                    Count = model.count,
                    IDequipment = model.drone,
                    Lat = model.lat,
                    Lon = model.lon,
                    Sended = false,
                    Sended_Datetime = null,
                    Altitude = (int)model.alt
                };


                var _Respuesta = DataClass.Create(User, data);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                {
                    await hubContext.Clients.All.SendAsync("LogReceived", _Respuesta.Mensaje);

                    return Ok();

                }
                return StatusCode(_Respuesta.StatusCode, _Respuesta.Mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }

}
