using HytaleList_Backend_API.Models;
using HytaleList_Backend_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        // POST: /Server/AddServer
        [HttpPost("AddServer")]
        [Authorize]
        public async Task<ActionResult<Server>> AddServer([FromBody] Server newServer)
        {
            var server = await _serverService.AddServer(newServer);
            if (server == null)
            {
                return BadRequest();
            }
            return Ok(server);
        }
    }
}
