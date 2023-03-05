using DAL.EntityModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_app.ViewModels.User;

namespace web_app.Hubs
{

    public class PresentationHub : Hub
    {
        private static List<string> hostId = new List<string>();
        private static Dictionary<string, List<UserJoinEventViewModel>> connectedUsers = new Dictionary<string, List<UserJoinEventViewModel>>();
        private ISlideService slideService;
        private IPresentationService presentationService;
        public PresentationHub(ISlideService slideService, IPresentationService presentationService)
        {
            this.slideService = slideService;
            this.presentationService = presentationService;

        }

        public async Task Join(string username, string image)
        {
            var presentationId = GetPresentationId();

            if (!connectedUsers.ContainsKey(presentationId))
            {
                connectedUsers.Add(presentationId, new List<UserJoinEventViewModel>());
            }

            if (!connectedUsers[presentationId].Any(u => u.Username == username))
            {
                connectedUsers[presentationId].Add(new UserJoinEventViewModel
                {
                    Username = username,
                    Image = image
                });
            }
            
            if (hostId.Count() == 0)
            {
                hostId.Add(Context.ConnectionId);
            }

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
        public async Task UpdateHostRating(int presentationId, int newRating) 
        {
            await Clients.Group(presentationId).SendAsync("React" ,username, reaction);
        }

        private string GetPresentationId()
        {
            var httpContext = Context.GetHttpContext();
            var presentationId = httpContext.Request.Query["presentationId"];
            return presentationId;
        }

        public async Task Submit(string answer)
        {
            await Clients.Caller.SendAsync("UpdateSelfAnswer", answer);
        }

        public async Task UpdateHostAnswers(string answer)
        {
            var host = hostId[0];
            await Clients.Client(host).SendAsync("UpdateHostAnswers", answer);
        }
    }
}
