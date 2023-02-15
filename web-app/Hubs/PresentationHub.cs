using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace web_app.Hubs
{
    public class PresentationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var presentationId = httpContext.Request.Query["presentationId"];
            await Groups.AddToGroupAsync(Context.ConnectionId, presentationId);
        }

        public Task UpdateSlide(int indexh, int indexv)
        {
            var httpContext = Context.GetHttpContext();
            var presentationId = httpContext.Request.Query["presentationId"];
            return Clients.Group(presentationId).SendAsync("UpdateSlide", indexh, indexv);
        }
    }
}
