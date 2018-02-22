using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetMusic.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class IndexController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return Ok("API started");
        }
    }
}