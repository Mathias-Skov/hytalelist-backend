using HytaleList_Backend_API.Data;
using HytaleList_Backend_API.Models;

namespace HytaleList_Backend_API.Services
{
    public class ServerService
    {
        private readonly ServerRepository _ServerRepository;
        public ServerService(ServerRepository ServerRepository)
        {
            _ServerRepository = ServerRepository;
        }

        public async Task<List<Server>> GetAllServers()
        {
            var servers = await _ServerRepository.GetAllServers();
            return servers;
        }

        public async Task<Server?> GetServerById(int id)
        {
            var server = await _ServerRepository.GetServerById(id);
            return server;
        }

        public async Task<Server?> AddServer(Server newServer)
        {
            // Add all business logic, valid ip, duplicate server etc.

            var server = new Server
            {
                Name = newServer.Name,
                IPAddress = newServer.IPAddress,
                Port = newServer.Port,
                Description = newServer.Description,
                PlayerCount = 0,
                MaxPlayers = newServer.MaxPlayers,
                Status = "Online",
                Votes = newServer.Votes,
                Tags = newServer.Tags
            };

            await _ServerRepository.AddServer(server);
            return server;
        }
    }
}
