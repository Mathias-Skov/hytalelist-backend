using HytaleList_Backend_API.Data;
using HytaleList_Backend_API.Models;
using System.Diagnostics;

namespace HytaleList_Backend_API.Services
{
    public class DiscordService
    {
        private readonly HttpClient _httpClient;
        private readonly string _webhookUrl;

        public DiscordService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _webhookUrl = configuration["Discord:WebhookUrl"]
                ?? throw new Exception("Missing Discord Webhook URL");
        }

        public async Task SendAnnouncement(Server server)
        {
            var payload = new
            {
                content = "** A new Hytale server has been added **",
                embeds = new[]
                {
                    new
                    {
                        title = $"Server Name: {server.Name}",
                        description = $"Description: {server.Description}",
                        tags = $"Tags: {server.Tags}",
                        color = 0x57F287,
                        fields = new[]
                        {
                            new { name = "Address", value = $"{server.IPAddress}:{server.Port}", inline = true }
                        },
                        footer = new
                        {
                            text = "HytaleList.io"
                        },
                        timestamp = DateTime.UtcNow
                    }
                }
            };

            var response = await _httpClient.PostAsJsonAsync(_webhookUrl, payload);
            response.EnsureSuccessStatusCode();
        }
    }
}
