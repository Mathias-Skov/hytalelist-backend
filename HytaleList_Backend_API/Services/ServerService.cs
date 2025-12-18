using HytaleList_Backend_API.Data;
using HytaleList_Backend_API.Models;

namespace HytaleList_Backend_API.Services
{
    public class ServerService
    {
        private readonly Repository _repository;
        public ServerService(Repository repository)
        {
            _repository = repository;
        }

        public async Task<List<Server>> GetAllServers()
        {
            var servers = await _repository.GetAllServers();
            return servers;
        }

        public async Task<Server?> GetServerById(int id)
        {
            var server = await _repository.GetServerById(id);
            return server;
        }
    }
}
