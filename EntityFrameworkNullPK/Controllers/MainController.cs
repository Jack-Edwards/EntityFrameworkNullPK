using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkNullPK.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        public MainController()
        { }

        [HttpGet]
        public string Get()
        {
            return "hello";
        }
    }
}