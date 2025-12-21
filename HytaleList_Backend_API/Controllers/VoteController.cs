using HytaleList_Backend_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HytaleList_Backend_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoteController : Controller
    {
        private readonly VoteService _voteService;

        public VoteController(VoteService voteService)
        {
            _voteService = voteService;
        }

        // GET: /Vote/CountServerVotes?id={id}
        [HttpGet("CountServerVotes/{id}")]
        public async Task<ActionResult<int>> GetTopVotesByServerId(int id)
        {
            var server = await _voteService.GetTopVotesByServerId(id);

            return Ok(server);
        }
    }
}
