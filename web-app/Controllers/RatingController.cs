using BAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using web_app.Hubs;
using web_app.ViewModels.Slide;

namespace API.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingService ratingService;
        private readonly PresentationHub presentationHub;

        public RatingController(IRatingService ratingService, PresentationHub presentationHub)
        {
            this.ratingService = ratingService;
            this.presentationHub = presentationHub;
        }

        [HttpGet("presentation/{presentationId}/average")]
        public async Task<IActionResult> GetAverageRating(int presentationId)
        {
            try
            {
                var averageRating = ratingService.CalculateAverageRating(presentationId);
                return Ok(averageRating);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResult> ClearVotes(int presentationId)
        {
            await this.ratingService.ClearVotes(presentationId);
            
            return new JsonResult(Ok());
        }

        [HttpPost]
        public async Task<JsonResult> Vote(int presentationId, int rating)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await this.ratingService.AddRating(presentationId, rating, userId);
            var resultRating = this.ratingService.CalculateAverageRating(presentationId);
            await presentationHub.UpdateHostRating(presentationId, resultRating);

            return new JsonResult(Ok());
        }


        [HttpPut]
        public async Task<JsonResult> EditRating([FromBody] SlideEditRatingViewModel viewModel)
        {
            var result = await this.ratingService.EditRating(viewModel.Id, viewModel.Rating);

            if (result is false)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok());
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteRating(int id)
        {
            var rating = await this.ratingService.GetById(id);

            if (rating is null)
            {
                return new JsonResult(NotFound());
            }
            await this.ratingService.Remove(id);

            return new JsonResult(Ok());
        }
    }
}
