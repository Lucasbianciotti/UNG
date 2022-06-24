using Microsoft.AspNetCore.Mvc;

namespace APIClient.Controllers
{
    [Route("")]
    [ApiController]
    public class HoraController : ControllerBase
    {

        [HttpGet("hora")]
        public IActionResult hora()
        {
            try
            {
                long unixTimeStampInSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();

                return Ok(unixTimeStampInSeconds);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
