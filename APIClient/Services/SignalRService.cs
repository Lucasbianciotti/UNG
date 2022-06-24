using Microsoft.AspNetCore.SignalR;

namespace APIClient.Services
{
    public class SignalRService : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("DataReceived");
        }
    }
}
