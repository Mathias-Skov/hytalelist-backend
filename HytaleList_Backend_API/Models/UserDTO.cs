using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HytaleList_Backend_API.Models
{
    public class UserDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}
