using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HytaleList_Backend_API.Models
{
    public class Server
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServerId { get; set; }
        public string? Name { get; set; }
        public string? IPAddress { get; set; }
        public int Port { get; set; }
        public string? Description { get; set; }
        public int PlayerCount { get; set; }
        public int MaxPlayers { get; set; }
        public string? Status { get; set; }
        public int Votes { get; set; }
        public string? Tags { get; set; }
        // Add server banner / logo maybe as a external URL og just as an image?
    }
}
