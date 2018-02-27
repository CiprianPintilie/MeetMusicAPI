using System;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            return Ok(await _userService.GetUser(id));
        }

        /// <summary>
        /// Get top user music tastes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/tastes")]
        public async Task<IActionResult> GetUserTastes(Guid id)
        {
            return Ok(await _userService.GetUserTastes(id));
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="userModelModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateUser([FromBody]UserModel userModelModel)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid data in model");
            var userId = await _userService.CreateUser(userModelModel);
            return Created($"{_apiUrl}/user/{userId.ToString()}", userId);
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userModelModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> CreateUser(Guid id, [FromBody]UserModel userModelModel)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid data in model");
            var userId = await _userService.UpdateUser(id, userModelModel);
            return Ok(userId);
        }


        /// <summary>
        /// Updates given user tastes using the given model
        /// </summary>
        [HttpPut]
        [Route("{id}/tastes")]
        public async Task<IActionResult> UpdateUserTastes(Guid userId, [FromBody] UserMusicModel[] models)
        {
            await _userService.UpdateUserTastes(userId, models);
            return Ok();
        }

        /// <summary>
        /// Synchronize user tastes 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/synchronize/tastes")]
        public async Task<IActionResult> SynchronizeUserTastes(Guid userId, [FromBody] SynchronizedMusicGenresModel[] models)
        {
            await _userService.SynchronizeUserTastes(userId, models);
            return Ok();
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid data in model");
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
