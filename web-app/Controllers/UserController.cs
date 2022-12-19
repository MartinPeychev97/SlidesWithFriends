﻿using DAL.EntityModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using web_app.ViewModels.User;
using DAL.Enums;
using DAL;
using System.Linq;
using BAL.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace web_app.Controllers
{
    public class UserController : Controller
    {
        private readonly SlidesDbContext db;
        private readonly SignInManager<SlidesUser> signInManager;
        private readonly UserManager<SlidesUser> userManager;
        private readonly IUserService _userService;

        public UserController(
            SlidesDbContext db,
            UserManager<SlidesUser> userManager,
            SignInManager<SlidesUser> signInManager,
            IUserService userService)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var generatedUsernames = await this._userService.GenerateUsernames();
            this.ViewData["GeneratedUsernames"] = generatedUsernames.ToArray();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new SlidesUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                Subscription = DAL.Enums.Subscription.None
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "User");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginFormViewModel model = new LoginFormViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Subscription()
        {
            return View();
        }

        public IActionResult Subscribe(Subscription subscriptionType)
        {
            var user = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            user.Subscription = subscriptionType;
            db.SaveChanges();
           
            return RedirectToAction("Subscription", "User");
        }
    }
}
