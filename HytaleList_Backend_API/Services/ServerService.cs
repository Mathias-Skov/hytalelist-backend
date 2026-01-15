using HytaleList_Backend_API.Data;
using HytaleList_Backend_API.Models;
using System.Diagnostics;

namespace HytaleList_Backend_API.Services
{
    public class ServerService
    {
        private readonly ServerRepository _serverRepository;
        private readonly DiscordService _discordService;

        public ServerService(ServerRepository serverRepository, DiscordService discordService)
        {
            _serverRepository = serverRepository;
            _discordService = discordService;
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

            await _serverRepository.AddServer(server);

            try
            {
                await _discordService.SendAnnouncement(server);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            
            return server;
        }
    }
}
