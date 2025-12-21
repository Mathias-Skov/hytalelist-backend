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
                var topVotes = await _dbContext.servers
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
    }
}
