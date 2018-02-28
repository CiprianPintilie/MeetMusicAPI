using System.Threading.Tasks;
using MeetMusic.Interfaces;
using MeetMusicModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetMusic.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ManagementController : Controller
    {
        private readonly IManagementService _managementService;

        public ManagementController(IManagementService managementService)
        {
            _managementService = managementService;
        }

        /// <summary>
        /// Get all genres
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("genres")]
        public async Task<IActionResult> GetAllGenres()
        {
            await _managementService.GetAllGenres();
            return Ok();
        }

        /// <summary>
        /// Get all families
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("families")]
        public async Task<IActionResult> GetAllFamilies()
        {
            await _managementService.GetAllFamilies();
            return Ok();
        }

        /// <summary>
        /// Updates music families, genres, and genre/family association, based on the given JSON model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("families")]
        public async Task<IActionResult> UpdateFamilies([FromBody] MusicFamilyUpdateModel[] model)
        {
            await _managementService.UpdateFamilies(model);
            return Ok();
        }
    }
}