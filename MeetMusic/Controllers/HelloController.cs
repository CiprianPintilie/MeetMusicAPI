using Microsoft.AspNetCore.Mvc;

namespace MeetMusic.Controllers
{
    [Route("api/[controller]")]
    public class HelloController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            return Ok("Hello");
        }

        [HttpGet]
        [Route("{name}")]
        public IActionResult Get(string name)
        {
            return Ok($"Hello {name}");
        }
    }
}