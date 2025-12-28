using HytaleList_Backend_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HytaleList_Backend_API.Data
{
    public class ServerRepository
    {
        private readonly MyDbContext _dbContext;

        public ServerRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Server>> GetAllServers()
        {
            try
            {
                var serverList = await _dbContext.Servers.ToListAsync();
                return serverList;
            } 
            catch (Exception ex)
            {
                Debug.WriteLine($"[ServerRepository]: GetAllServers() - Exception: {ex.Message}");
                return new List<Server>();
            }
        }

        public async Task<Server?> GetServerById(int id)
        {
            try
            {
                var server = await _dbContext.Servers.FindAsync(id);
                return server;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ServerRepository]: GetServerById({id}) - Exception: {ex.Message}");
                return null;
            }
        }
    }
}
