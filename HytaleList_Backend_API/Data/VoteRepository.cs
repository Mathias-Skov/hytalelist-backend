using HytaleList_Backend_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HytaleList_Backend_API.Data
{
    public class VoteRepository
    {
        private readonly MyDbContext _dbContext;
        public VoteRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetTopVotesByServerId(int serverId)
        {
            try
            {
                // Might want to seperate into ServerRepository on sight
                var topVotes = await _dbContext.Servers
                    .Where(v => v.ServerId == serverId)
                    .Select(v => v.Votes)
                    .FirstOrDefaultAsync();

                return topVotes;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[VoteRepository]: GetTopVotesByServerId(serverId) - Exception: {ex.Message}");
                return 0;
            }
        }

        public async Task<bool> UpdateServerVotes(Server server, Vote vote)
        {
            try
            {
                // Might want to seperate into ServerRepository on sight
                _dbContext.Servers.Update(server);
                
                _dbContext.Votes.Add(vote);

                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[VoteRepository]: UpdateServerVotes(server) - Exception: {ex.Message}");
                return false;
            }
        }
    }
}
