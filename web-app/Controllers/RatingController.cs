using BAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using web_app.ViewModels.Rating;
using web_app.ViewModels.Slide;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService ratingService;

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        [HttpGet("presentation/{presentationId}/average")]
        public async Task<IActionResult> GetAverageRating(int presentationId)
        {
            try
            {
                var averageRating = await ratingService.CalculateAverageRating(presentationId);
                return Ok(averageRating);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("presentation/{presentationId}")]
        public async Task<IActionResult> AddRating([FromBody] SlideRatingViewModel viewModel)
        {
            var rating = await this.ratingService.AddRating(viewModel.PresentationId, viewModel.Rating,viewModel.UserId);

            if (rating is null)
            {
                return new JsonResult(NotFound());
            }
            var ratingViewModel = new RatingViewModel
            {
                UserId = viewModel.UserId,
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
