using API.LocalClass;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models.Request;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DisparosController : ControllerBase
    {
        private readonly IHubContext<DataService> hubContext;

        public DisparosController(IHubContext<DataService> surveyHub)
        {
            this.hubContext = surveyHub;
        }

        [HttpPost("Disparo")]
        public async Task<IActionResult> Disparo([FromBody] Disparo_Request model)
        {
            try
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(model.timestamp * 1000);
                DateTime datetime = new(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second);

                var data = new Data_Request()
                {
                    Timespan = model.timestamp,
                    Datetime = datetime,
                    Info = model.data,
                    Count = model.count,
                    IDequipment = model.drone,
                };


                var _Respuesta = DataClass.Create(User, data);
                if (_Respuesta.StatusCode == StatusCodes.Status200OK || _Respuesta.StatusCode == StatusCodes.Status201Created)
                {
                    await hubContext.Clients.All.SendAsync("DataReceived", DataClass.PartialInformation(User, new Filter_Request()));

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
