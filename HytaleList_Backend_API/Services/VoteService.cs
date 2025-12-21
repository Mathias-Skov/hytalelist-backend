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

        public async Task<bool> SubmitVote(int serverId, string username)
        {
            var server = await _serverService.GetServerById(serverId);
            if (server == null)
            {
                return false;
            }
            // On sight make sure the repository updates the vote in case of concurrency votes, instead of using the nav property here.
            server.Votes += 1;

            var vote = new Vote
            {
                ServerId = serverId,
                Username = username,
                VoteDate = DateTime.UtcNow
            };

            var result = await _voteRepository.UpdateServerVotes(server, vote);
            return result;
        }
    }
}
