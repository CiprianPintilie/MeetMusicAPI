using System;
using MeetMusic.Interfaces;
using MeetMusicModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MeetMusic.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly string _apiUrl;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            _userService = userService;
            _apiUrl = configuration.GetSection("ApiInfo")["ApiUrl"];
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(Guid id)
        {
            return Ok(_userService.GetUser(id));
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IActionResult CreateUser([FromBody]User userModel)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid data in model");
            var userId = _userService.CreateUser(userModel);
            return Created($"{_apiUrl}/user/{userId.ToString()}", userId);
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid data in model");
            _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
