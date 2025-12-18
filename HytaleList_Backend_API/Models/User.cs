using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HytaleList_Backend_API.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Username { get; set; } 
        public string? PasswordHash { get; set; }
        public string? Salt { get; set; }
        public string? Email { get; set; }
        public string? EmailVerified { get; set; }
        public DateTime DateCreated { get; set; }
        public int ListedServers { get; set; }
    }
}
