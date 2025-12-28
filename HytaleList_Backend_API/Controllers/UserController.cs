using HytaleList_Backend_API.Models;
using HytaleList_Backend_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

        // GET /User/Details
        [HttpGet("Details")]
        [Authorize]
        public async Task<ActionResult> GetUserDetails()
        {
            // Get the userId from the JWT claims
            var sub = User.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrWhiteSpace(sub))
                return Unauthorized("User identifier not found in token.");

            if(!int.TryParse(sub, out int userId))
                return BadRequest("Invalid user identifier.");

            // Lookup the user in the database
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Return safe data only
            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email,
                user.DateCreated,
                user.EmailVerified,
                user.ListedServers
            });
        }

        // POST /User/Login
        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody] UserDTO userDto)
        {
            if(userDto.Username == null || userDto.Password == null)
            {
                return BadRequest("Username and password are required.");
            }

            var user = await _userService.AuthenticateUser(userDto);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(user);
        }

        // POST: /User/CreateUser
        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserDTO userDto)
        {
            if(userDto.Username == null || userDto.Password == null || userDto.Email == null)
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
