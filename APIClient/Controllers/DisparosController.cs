using APIClient.LocalClass;
using APIClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models.Request;

namespace APIClient.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DisparosController : ControllerBase
    {
        private readonly IHubContext<SignalRService> hubContext;

        public DisparosController(IHubContext<SignalRService> surveyHub)
        {
            this.hubContext = surveyHub;
        }

        [HttpPost("Disparo")]
        public async Task<IActionResult> Disparo([FromBody] Disparo_Request model)
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
                    Altitude = model.alt
                };


                var _Respuesta = DataClass.Create(User, data);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                {
                    await hubContext.Clients.All.SendAsync("DataReceived", _Respuesta.Mensaje);

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
