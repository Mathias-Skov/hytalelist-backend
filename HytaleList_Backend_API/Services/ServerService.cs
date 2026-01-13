using HytaleList_Backend_API.Data;
using HytaleList_Backend_API.Models;
using System.Diagnostics;

namespace HytaleList_Backend_API.Services
{
    public class ServerService
    {
        private readonly ServerRepository _serverRepository;
        public ServerService(ServerRepository serverRepository) 
        {
            _serverRepository = serverRepository;
        }

        public async Task<List<Server>> GetAllServers()
        {
            var servers = await _serverRepository.GetAllServers();
            return servers;
        }

        public async Task<Server?> GetServerById(int id)
        {
            var server = await _serverRepository.GetServerById(id);
            return server;
        }

        public async Task<Server?> GetServerByUserId(int userId)
        {
            return await _serverRepository.GetServerByUserId(userId);
        }

        public async Task<Server?> AddServer(Server newServer, int userId)
        {
            Debug.WriteLine($"[Service] Creating server with UserId: {userId}");

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
                Tags = newServer.Tags,
                UserId = userId
            };

            Debug.WriteLine($"[Service] Server UserId before repository: {server.UserId}");

            await _serverRepository.AddServer(server);

            Debug.WriteLine($"[Service] Server UserId after repository: {server.UserId}");

            return server;
        }
    }
}
