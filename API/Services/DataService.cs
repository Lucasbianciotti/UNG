using Microsoft.AspNetCore.SignalR;

namespace API.Services
{
    public class DataService : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("DataReceived");
        }
        //public async Task LeaveSurveyGroup(Guid surveyId)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, surveyId.ToString());
        //}
    }
}
