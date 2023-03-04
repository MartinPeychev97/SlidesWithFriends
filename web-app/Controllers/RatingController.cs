using BAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using web_app.Hubs;
using web_app.ViewModels.Rating;
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
        public async Task<IActionResult> AddRating( SlideRatingViewModel viewModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var rating = await this.ratingService.AddRating(viewModel.PresentationId, viewModel.Rating, userId);
            var resultRating = this.ratingService.CalculateAverageRating(viewModel.PresentationId);
            await presentationHub.UpdateHostRating(viewModel.PresentationId, resultRating);

            if (rating is null)
            {
                return new JsonResult(NotFound());
            }
            var ratingViewModel = new RatingViewModel
            {
                UserId = userId,
                PresentationId = viewModel.PresentationId,
                Rating = rating.value
            };

            return new JsonResult(ratingViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating([FromBody] SlideEditRatingViewModel viewModel)
        {
            var result = await this.ratingService.EditRating(viewModel.Id, viewModel.Rating);

            if (result is false)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
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
