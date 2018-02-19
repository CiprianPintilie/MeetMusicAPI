using Microsoft.AspNetCore.Mvc;

namespace MeetMusic.Controllers
{
    [Route("api/[controller]")]
    public class IndexController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return Ok("API started");
        }
    }
}