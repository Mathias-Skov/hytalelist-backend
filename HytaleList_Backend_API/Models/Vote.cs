using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HytaleList_Backend_API.Models
{
    public class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoteId { get; set; }
        public int ServerId { get; set; }
        public string Username { get; set; } = string.Empty;   // gem lowercase/trim
        public DateTime VoteDate { get; set; } = DateTime.UtcNow;
        public string IpHash { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;

        public Server? Server { get; set; }
    }
}
