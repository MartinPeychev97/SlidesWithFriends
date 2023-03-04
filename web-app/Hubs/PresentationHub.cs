using BAL.Interfaces;
using BAL.Services;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
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
        private static Dictionary<string, List<UserJoinEventViewModel>> connectedUsers = new Dictionary<string, List<UserJoinEventViewModel>>();
        private ISlideService slideService;
        private IPresentationService presentationService;
        private SlidesDbContext db;
        public PresentationHub(ISlideService slideService,IPresentationService presentationService,SlidesDbContext db )
        {
            this.slideService = slideService;
            this.presentationService = presentationService;
            this.db = db;

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

        public async Task React(string username, string reaction)
        {
            var presentationId = GetPresentationId();
            await Clients.Group(presentationId).SendAsync("React" ,username, reaction);
        }

        private string GetPresentationId()
        {
            var httpContext = Context.GetHttpContext();
            var presentationId = httpContext.Request.Query["presentationId"];
            return presentationId;
        }
        [HttpPost]
        public async Task Submit(string answer , string url)
        {
            var presentationId = GetPresentationId();
            var presentation = await presentationService.GetByIdWordCloud(int.Parse(presentationId));

            var slideId = int.Parse(url.Split("/").Last());
            var slide = presentation.Slides.ToList()[slideId - 1];
            await slideService.ClearAnswers();
            await slideService.AddAnswerToWordCloud(answer, slide);

            await Clients.Group(presentationId).SendAsync("Update");
        }
    }
}
