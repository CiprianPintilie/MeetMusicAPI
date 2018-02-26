using System.Threading.Tasks;
using MeetMusic.Interfaces;
using MeetMusicModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetMusic.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ManagementController : Controller
    {
        private readonly IManagementService _managementService;

        public ManagementController(IManagementService managementService)
        {
            _managementService = managementService;
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateFamilies([FromBody] MusicFamilyUpdateModel[] model)
        {
            await _managementService.UpdateFamilies(model);
            return Ok();
        }
    }
}