using MeetMusic.Interfaces;
using MeetMusicModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetMusic.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet(Name = nameof(GetUser))]
        [Route("{id}")]
        public IActionResult GetUser(string id)
        {
            return Ok(_userService.GetAllUsers());
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser([FromBody]User userModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            _userService.CreateUser(userModel);
            return Created("???" , nameof(GetUser));
        }
    }
}
