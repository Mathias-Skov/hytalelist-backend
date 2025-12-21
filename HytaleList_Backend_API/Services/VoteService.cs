using HytaleList_Backend_API.Data;

namespace HytaleList_Backend_API.Services
{
    public class VoteService
    {
        private readonly VoteRepository _voteRepository;

        public VoteService(VoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<int> GetTopVotesByServerId(int serverId)
        {
            var topVotes = await _voteRepository.GetTopVotesByServerId(serverId);
            return topVotes;
        }
    }
}
