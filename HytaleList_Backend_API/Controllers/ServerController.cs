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

        [HttpGet]
        public ActionResult<List<Server>> Get()
        {
            var servers = _serverService.GetAllServers();
            return Ok(servers);
        }
    }
}
