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
        public List<Server> GetAllServers()
        {
            var servers = _repository.GetAllServers();
            return servers;
        }

        public Server? GetServerById(int id)
        {
            var server = _repository.GetServerById(id);
            return server;
        }
    }
}
