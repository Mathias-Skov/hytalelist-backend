using HytaleList_Backend_API.Models;
using System.Diagnostics;

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
                    ServerId = 2,
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
            try
            {
                return serverList;
            } 
            catch (Exception ex)
            {
                Debug.WriteLine($"[Repository]: GetAllServers() - Exception: {ex.Message}");
                return new List<Server>();
            }
        }

        public Server? GetServerById(int id)
        {
            try
            {
                Server? specificServer = serverList.Find(s => s.ServerId == id);
                return specificServer;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Repository]: GetServerById({id}) - Exception: {ex.Message}");
                return null;
            }
        }

        public Server? GetServerByIdUsingLoop(int id)
        {
            try
            {
                int i = 0;
                while (i < serverList.Count)
                {
                    Server specificServer = serverList[i];
                    if (specificServer.ServerId == id)
                    {
                        return specificServer;
                    }
                    i++;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Repository]: GetServerByIdUsingLoop({id}) - Exception: {ex.Message}");
                return null;
            }
        }
    }
}
