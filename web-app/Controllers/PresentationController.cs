﻿using BAL.Interfaces;
using DAL.EntityModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using web_app.ViewModels.Presentation;
using web_app.ViewModels.Slide;

namespace web_app.Controllers
{
    public class PresentationController : Controller
    {
        private readonly IPresentationService presentationService;

        public PresentationController(IPresentationService presentationService)
        {
            this.presentationService = presentationService;
        }
        public IActionResult PresentationIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PresentationCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
               
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await this.presentationService.Create(viewModel.Name, userId);

            return RedirectToAction("PresentationIndex" ,"Presentation");
        }


        [HttpGet]
        public  IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetById([FromQuery] int id)
        {
            var presentation = await this.presentationService.GetById(id);
            var slides = presentation.Slides
                .Select(s => new SlideViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Text = s.Text,
                    Image = s.Image,
                    Background = s.Background,
                    Type = s.Type.ToString(),
                    Rating= s.Rating,
                }).ToList();

            var presentationViewModel = new PresentationEditViewModel()
            {
                Id = presentation.Id,
                Name = presentation.Name,
                Slides = slides
            };

            return new JsonResult(presentationViewModel);
        }

        [HttpPut]
        public async Task<JsonResult> EditName([FromBody] PresentationNameViewModel viewModel)
        {
            await this.presentationService.EditName(viewModel.Id, viewModel.Name);

            return new JsonResult(Ok());
        }

        public async Task<IActionResult> Remove(int id)
        {
            var presentation = await this.presentationService.GetById(id);
            if (presentation is null)
            {
                return new JsonResult(NotFound());
            }

            await this.presentationService.Remove(presentation.Id);

            return RedirectToAction("PresentationIndex", "Presentation");
        }
    }
}
