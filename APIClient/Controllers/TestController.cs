using APIClient.LocalModels.SQLite;
using APIClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIClient.Controllers
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
            string URL;

            if (HttpContext.Request.IsHttps)
                URL = "https://" + HttpContext.Request.Host.Value + "/";
            else
                URL = "http://" + HttpContext.Request.Host.Value + "/";

            var db = new Local_Context();
            URL += "\n\n" + db.DbPath;

            return Ok(URL);
        }

    }

}
