using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace web_app.Hubs
{
    public class PresentationHub : Hub
    {
        public Task UpdateSlide(int indexh, int indexv)
        {
            return Clients.All.SendAsync("UpdateSlide", indexh, indexv);
        }
    }
}
