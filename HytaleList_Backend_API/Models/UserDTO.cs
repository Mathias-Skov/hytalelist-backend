using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HytaleList_Backend_API.Models
{
    public class UserDTO
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
    }
}
