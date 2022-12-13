﻿using BAL.Interfaces;
using Common.InputModels.Presentation;
using Microsoft.AspNetCore.Mvc;
using System;
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
                    Text = s.Text,
                    PresentationBackground = presentation.Background
                });

            var presentationViewModel = new PresentationEditViewModel()
            {
                Id = presentation.Id,
                Name = presentation.Name,
                Slides = slides
            };

            return View(presentationViewModel);
        }


        [HttpPut]
        public async Task<IActionResult> EditBackground(PresentationBackgroundInputModel model)
        {
            try
            {
                await this.presentationService.EditBackground(model);

                return Ok(model.Background);
            }
            catch (Exception e)
            {
                //TODO: Talk about different errors being thrown from service.
                return NotFound();
            }
        }
    }
}