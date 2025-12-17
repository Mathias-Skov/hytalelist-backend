using HytaleList_Backend_API.Models;

namespace HytaleList_Backend_API.Data
{
    public class Repository
    {
        private List<Server> serverList = new List<Server>();
        public Repository()
        {
            if (serverList?.Count == 0)
            {
                Server s1 = new Server()
                {
                    ServerId = 1,
                    Name = "Hytale Fun Server",
                    IPAddress = "127.0.0.1",
                    Port = 25565,
                    Description = "A fun Hytale server for everyone!",
                    PlayerCount = 10,
                    MaxPlayers = 100,
                    Status = "Online"
                };

                Server s2 = new Server()
                {
                    ServerId = 1,
                    Name = "Hytale.dk | Dansk Hytale Server |",
                    IPAddress = "spil.hytale.dk",
                    Port = 25565,
                    Description = "Et dansk community",
                    PlayerCount = 0,
                    MaxPlayers = 50,
                    Status = "Offline"
                };

                serverList.Add(s1);
                serverList.Add(s2);
            }
        }

        public List<Server> GetAllServers()
        {
            return serverList;
        }
    }
}
