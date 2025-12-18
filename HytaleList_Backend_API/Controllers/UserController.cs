using HytaleList_Backend_API.Models;
using HytaleList_Backend_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HytaleList_Backend_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // POST: /User/CreateUser
        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> CreateUser(string username, string passwordHash, string email)
        {
            var createUser = await _userService.CreateUser(username, passwordHash, email);
            if (!createUser)
            {
                return BadRequest("User could not be created.");
            }
            else
            {                 
                return Ok("User created successfully.");
            }
        }
    }
}
