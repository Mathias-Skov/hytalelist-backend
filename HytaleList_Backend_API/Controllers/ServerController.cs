using HytaleList_Backend_API.Models;
using HytaleList_Backend_API.Services;
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
        public ActionResult<List<Server>> Get()
        {
            var servers = _serverService.GetAllServers();
            if (servers.Count == 0)
            {
                return NotFound();
            }
            return Ok(servers);
        }

        // GET: /Server/GetServerById/{id}
        [HttpGet("GetServerById/{id}")]
        public ActionResult<Server> GetServerById(int id)
        {
            var server = _serverService.GetServerById(id);
            if (server == null)
            {
                return NotFound();
            }
            return Ok(server);
        }
    }
}
