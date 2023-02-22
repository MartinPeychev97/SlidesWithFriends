using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_app.ViewModels.User;

namespace web_app.Hubs
{
    public class PresentationHub : Hub
    {
        private static Dictionary<string, List<UserJoinEventViewModel>> connectedUsers = new Dictionary<string, List<UserJoinEventViewModel>>();

        public async Task Join(string username, string image)
        {
            var presentationId = GetPresentationId();

            if (!connectedUsers.ContainsKey(presentationId))
            {
                connectedUsers.Add(presentationId, new List<UserJoinEventViewModel>());
            }
            connectedUsers[presentationId].Add(new UserJoinEventViewModel
            {
                Username = username,
                Image = image
            });

            await Groups.AddToGroupAsync(Context.ConnectionId, presentationId);
            await Clients.Group(presentationId).SendAsync("DisplayUsers", connectedUsers[presentationId]);
        }

        public async Task Leave(string username)
        {
            var presentationId = GetPresentationId();
            connectedUsers[presentationId].Remove(connectedUsers[presentationId].FirstOrDefault(u => u.Username == username));
            await Clients.Group(presentationId).SendAsync("DisplayUsers", connectedUsers[presentationId]);
        }


        public async Task UpdateSlide(int indexh, int indexv)
        {
            var presentationId = GetPresentationId();
            await Clients.Group(presentationId).SendAsync("UpdateSlide", indexh, indexv);
        }

        private string GetPresentationId()
        {
            var httpContext = Context.GetHttpContext();
            var presentationId = httpContext.Request.Query["presentationId"];
            return presentationId;
        }
    }
}
