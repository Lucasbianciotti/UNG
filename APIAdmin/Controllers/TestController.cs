using APIAdmin.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIAdmin.Controllers
{
    [Route("")]
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
            return Ok("API ADMIN iniciada correctamente");
        }
    }

}
