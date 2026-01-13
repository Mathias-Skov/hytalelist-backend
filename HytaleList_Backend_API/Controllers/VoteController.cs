using HytaleList_Backend_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace HytaleList_Backend_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoteController : Controller
    {
        private readonly VoteService _voteService;
        private readonly IConfiguration _configuration;

        public VoteController(VoteService voteService, IConfiguration configuration)
        {
            _voteService = voteService;
            _configuration = configuration;
        }

        // GET: /Vote/CountServerVotes?id={id}
        [HttpGet("CountServerVotes/{id}")]
        public async Task<ActionResult<int>> GetTopVotesByServerId(int id)
        {
            var server = await _voteService.GetTopVotesByServerId(id);

            return Ok(server);
        }

        // POST: /Vote/SubmitVote/{id}
        [HttpPost("SubmitVote/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> SubmitVote([FromRoute] int id, [FromBody] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest("Username is required.");

            // Hent reel klient-IP bag Cloudflare
            string clientIp =
                Request.Headers["CF-Connecting-IP"].FirstOrDefault()
                ?? HttpContext.Connection.RemoteIpAddress?.ToString()
                ?? "unknown";

            string userAgent = Request.Headers.UserAgent.ToString();
            string ipHash = HashIp(clientIp, _configuration["IpSalt"]);

            var (ok, error) = await _voteService.SubmitVote(id, username, ipHash, userAgent);
            if (!ok) return BadRequest(error);

            return Ok("Vote submitted successfully.");
        }

        private string HashIp(string ip, string salt)
        {
            using var sha = SHA256.Create();
            return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes($"{ip}-{salt}")));
        }
    }
}
