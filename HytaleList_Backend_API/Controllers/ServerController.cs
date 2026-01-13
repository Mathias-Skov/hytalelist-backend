using HytaleList_Backend_API.Models;
using HytaleList_Backend_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HytaleList_Backend_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerController : Controller
    {
        private readonly ServerService _serverService;
        public ServerController(ServerService serverSevice)
        {
            _serverService = serverSevice;
        }

        // GET: /Server/GetAllServers
        [HttpGet("GetAllServers")]
        public async Task<ActionResult<List<Server>>> Get()
        {
            var servers = await _serverService.GetAllServers();
            if (servers.Count == 0)
            {
                return NotFound();
            }
            return Ok(servers);
        }

        // GET: /Server/GetServerById/{id}
        [HttpGet("GetServerById/{id}")]
        public async Task<ActionResult<Server>> GetServerById(int id)
        {
            var server = await _serverService.GetServerById(id);
            if (server == null)
            {
                return NotFound();
            }
            return Ok(server);
        }

        [HttpPost("AddServer")]
        [Authorize]
        public async Task<ActionResult<Server>> AddServer([FromBody] Server newServer)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("User not found in token");
            }

            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized("Invalid user ID");
            }

            var existingServer = await _serverService.GetServerByUserId(userId);
            if (existingServer != null)
            {
                return BadRequest("You can only add one server");
            }

            var server = await _serverService.AddServer(newServer, userId); // Send userId her
            if (server == null)
            {
                return BadRequest();
            }
            return Ok(server);
        }
    }
}
