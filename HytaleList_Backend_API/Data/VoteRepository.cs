using HytaleList_Backend_API.Data;
using HytaleList_Backend_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            return await _dbContext.Servers
                .Where(v => v.ServerId == serverId)
                .Select(v => v.Votes)
                .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[VoteRepository]: GetTopVotesByServerId(serverId) - Exception: {ex.Message}");
            return 0;
        }
    }

    public async Task<Vote?> GetByUserToday(int serverId, string username, DateOnly today)
    {
        var u = username.Trim().ToLower();
        var todayDateTime = today.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        var tomorrowDateTime = today.AddDays(1).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);

        return await _dbContext.Votes
            .FirstOrDefaultAsync(v => v.ServerId == serverId
                                   && v.Username == u
                                   && v.VoteDate >= todayDateTime
                                   && v.VoteDate < tomorrowDateTime);
    }

    public async Task<int> CountByIpToday(int serverId, string ipHash, DateOnly today)
    {
        var todayDateTime = today.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        var tomorrowDateTime = today.AddDays(1).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);

        return await _dbContext.Votes
            .CountAsync(v => v.ServerId == serverId
                          && v.IpHash == ipHash
                          && v.VoteDate >= todayDateTime
                          && v.VoteDate < tomorrowDateTime);
    }

    public async Task<bool> AddVote(Vote vote, Server server)
    {
        try
        {
            _dbContext.Servers.Update(server);
            _dbContext.Votes.Add(vote);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[VoteRepository]: AddVote - {ex.Message}");
            return false;
        }
    }

    public async Task<bool> HasIpVotedAnyServerToday(string ipHash, DateOnly today)
    {
        var todayDateTime = today.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        var tomorrowDateTime = today.AddDays(1).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);

        return await _dbContext.Votes
            .AnyAsync(v => v.IpHash == ipHash
                       && v.VoteDate >= todayDateTime
                       && v.VoteDate < tomorrowDateTime);
    }
}