using HytaleList_Backend_API.Data;
using HytaleList_Backend_API.Models;

namespace HytaleList_Backend_API.Services
{
    public class VoteService
    {
        private readonly VoteRepository _voteRepository;
        private readonly ServerService _serverService;

        public VoteService(VoteRepository voteRepository, ServerService serverService)
        {
            _voteRepository = voteRepository;
            _serverService = serverService;
        }

        public async Task<int> GetTopVotesByServerId(int serverId)
        {
            var topVotes = await _voteRepository.GetTopVotesByServerId(serverId);
            return topVotes;
        }

        public async Task<(bool ok, string? error)> SubmitVote(int serverId, string username, string ipHash, string userAgent)
        {
            var server = await _serverService.GetServerById(serverId);
            if (server == null) return (false, "Server not found");

            var normalized = username.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(normalized)) return (false, "Username required");

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var existing = await _voteRepository.GetByUserToday(serverId, normalized, today);
            if (existing != null) return (false, "You have already voted for this server today.");

            // Tjek om IP har stemt på NOGEN server i dag (på tværs af alle servere)
            var hasVotedToday = await _voteRepository.HasIpVotedAnyServerToday(ipHash, today);
            if (hasVotedToday) return (false, "You can only vote for one server per day.");

            server.Votes += 1;

            var vote = new Vote
            {
                ServerId = serverId,
                Username = normalized,
                VoteDate = DateTime.UtcNow,
                IpHash = ipHash,
                UserAgent = userAgent
            };

            var saved = await _voteRepository.AddVote(vote, server);
            return saved ? (true, null) : (false, "Failed to submit vote.");
        }
    }
}
