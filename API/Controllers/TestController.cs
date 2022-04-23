using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IEncrypterService _encrypterService;

        public TestController(IEncrypterService encrypterService)
        {
            _encrypterService = encrypterService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API iniciada correctamente");
        }
    }

}
