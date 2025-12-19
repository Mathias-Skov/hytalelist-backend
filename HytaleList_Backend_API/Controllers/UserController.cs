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
        public async Task<ActionResult<User>> Login([FromBody] UserDTO userDto)
        {
            if(userDto.Username == null || userDto.PasswordHash == null)
            {
                return BadRequest("Username and password are required.");
            }

            var user = await _userService.AuthenticateUser(userDto);

            if (user == false)
            {
                return Unauthorized("Invalid username or password.");
            }

            //string token = _userService.CreateJWT(user);

            return Ok(user);
        }

        // POST: /User/CreateUser
        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserDTO userDto)
        {
            if(userDto.Username == null || userDto.PasswordHash == null || userDto.Email == null)
            {
                return BadRequest("Username, password, and email are required.");
            }

            var created = await _userService.CreateUser(userDto);

            if (!created)
            {
                return BadRequest("User could not be created.");
            }

            return Ok("User created successfully.");
        }
    }
}
