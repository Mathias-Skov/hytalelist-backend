using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HytaleList_Backend_API.Models
{
    public class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoteId { get; set; }
        public int ServerId { get; set; }
        public string? Username { get; set; }
        public DateTime VoteDate { get; set; }
        public Server? Server { get; set; }
    }
}
