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
    }
}
