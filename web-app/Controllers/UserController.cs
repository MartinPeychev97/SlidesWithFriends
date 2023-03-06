using DAL;
using DAL.EntityModels.User;
using DAL.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using web_app.ViewModels.User;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;

namespace web_app.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly SlidesDbContext db;
        private readonly SignInManager<SlidesUser> signInManager;
        private readonly UserManager<SlidesUser> userManager;
        private readonly IWebHostEnvironment hostEnvironment;

        public UserController(
            SlidesDbContext db,
            UserManager<SlidesUser> userManager,
            SignInManager<SlidesUser> signInManager,
            IWebHostEnvironment hostEnvironment)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imagePath = string.Empty;

            if (model.Image != null)
            {
                string wwwroot = hostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwroot, @"images", "users");
                var extension = Path.GetExtension(model.Image.FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }

                imagePath = @"\images\users\" + fileName + extension;
            }
            else
            {
                imagePath = @"\images\users\avatar-default-icon.png";
            }

            var user = new SlidesUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                Subscription = DAL.Enums.Subscription.None,
                Image = imagePath,
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
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Subscription()
        {
            return View();
        }

        public async Task<IActionResult> Subscribe(Subscription subscriptionType)
        {
            var user = await this.userManager.FindByNameAsync(User.Identity.Name);
            user.Subscription = subscriptionType;
            db.SaveChanges();

            return RedirectToAction("PresentationIndex", "Presentation");
        }
    }
}
