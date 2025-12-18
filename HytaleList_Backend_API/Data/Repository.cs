using HytaleList_Backend_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HytaleList_Backend_API.Data
{
    public class Repository
    {
        private readonly MyDbContext _dbContext;

        public Repository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Server>> GetAllServers()
        {
            try
            {
                var serverList = await _dbContext.servers.ToListAsync();
                return serverList;
            } 
            catch (Exception ex)
            {
                Debug.WriteLine($"[Repository]: GetAllServers() - Exception: {ex.Message}");
                return new List<Server>();
            }
        }

        public async Task<Server?> GetServerById(int id)
        {
            try
            {
                var server = await _dbContext.servers.FindAsync(id);
                return server;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Repository]: GetServerById({id}) - Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<Server?> GetServerByIdUsingLoop(int id)
        {
            try
            {
                var servers = await _dbContext.servers.ToListAsync();

                int i = 0;
                while (i < servers.Count)
                {
                    Server? server = servers[i];
                    if (server.ServerId == id)
                    {
                        return server;
                    }
                    i++;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Repository]: GetServerByIdUsingLoop({id}) - Exception: {ex.Message}");
                return null;
            }
        }
    }
}
