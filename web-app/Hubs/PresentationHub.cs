using DAL.EntityModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace web_app.Hubs
{
    public class PresentationHub : Hub
    {
        private static Dictionary<string, string> connectedUsers = new Dictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            var presentationId = GetPresentationId();

            await Groups.AddToGroupAsync(Context.ConnectionId, presentationId);
        }

        public async Task Join(string username, string image)
        {
            var presentationId = GetPresentationId();
            connectedUsers.Add(username, image);
            await Clients.Group(presentationId).SendAsync("UserJoined", connectedUsers);
        }

        public async Task Leave(string username)
        {
            var presentationId = GetPresentationId();
            connectedUsers.Remove(username);
            await Clients.Group(presentationId).SendAsync("UserLeft", connectedUsers);
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
