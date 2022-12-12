﻿using BAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var presentation = await this.presentationService.GetById(id);
            var slides = presentation.Slides
                .Select(s => new SlideViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Text = s.Text
                });

            var presentationViewModel = new PresentationEditViewModel()
            {
                Name = presentation.Name,
                Slides = slides
            };

            return View(presentationViewModel);
        }
    }
}
