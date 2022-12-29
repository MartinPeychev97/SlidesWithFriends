using DAL;
using DAL.EntityModels.User;
using DAL.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using web_app.ViewModels.User;

namespace web_app.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly SlidesDbContext db;
        private readonly SignInManager<SlidesUser> signInManager;
        private readonly UserManager<SlidesUser> userManager;

        public UserController(
            SlidesDbContext db,
            UserManager<SlidesUser> userManager,
            SignInManager<SlidesUser> signInManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterFormViewModel();

            return View(model);
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
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            
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
