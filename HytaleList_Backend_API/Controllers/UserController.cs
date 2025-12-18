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

        // POST /User/Login
        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromQuery] string username, [FromQuery] string password)
        {
            var user = await _userService.AuthenticateUser(username, password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(user);
        }

        // POST: /User/CreateUser
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromQuery] string username, [FromQuery] string password, [FromQuery] string email)
        {
            var created = await _userService.CreateUser(username, password, email);
            if (!created)
            {
                return BadRequest("User could not be created.");
            }
            return Ok("User created successfully.");
        }
    }
}
