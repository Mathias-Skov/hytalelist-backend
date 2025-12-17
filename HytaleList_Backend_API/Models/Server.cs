namespace HytaleList_Backend_API.Models
{
    public class Server
    {
        public int ServerId { get; set; }
        public string? Name { get; set; }
        public string? IPAddress { get; set; }
        public int Port { get; set; }
        public string? Description { get; set; }
        public int PlayerCount { get; set; }
        public int MaxPlayers { get; set; }
        public string? Status { get; set; }
    }
}
