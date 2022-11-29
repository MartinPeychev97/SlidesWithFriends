using DAL.EntityModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using web_app.ViewModels.User;

namespace web_app.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<SlidesUser> signInManager;
        private readonly UserManager<SlidesUser> userManager;

        public UserController(
            UserManager<SlidesUser> userManager,
            SignInManager<SlidesUser> signInManager)
        {
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
    }
}
