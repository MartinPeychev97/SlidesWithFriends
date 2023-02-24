using DAL.EntityModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace web_app.Controllers
{
    [AllowAnonymous, Route("GoogleAuth")]
    public class GoogleAuthController : Controller
    {
        private readonly UserManager<SlidesUser> userManager;
        private readonly SignInManager<SlidesUser> signInManager;

        public GoogleAuthController(UserManager<SlidesUser> userMgr, SignInManager<SlidesUser> signinMgr)
        {
            userManager = userMgr;
            signInManager = signinMgr;
        }

        [Route("GoogleLogin")]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "GoogleAuth");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [Route("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction("Index", "Home");

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
            {
                var userName = info.Principal.FindFirst(ClaimTypes.Email).Value;
                userName = userName.Substring(0, userName.IndexOf("@"));

                SlidesUser user = new SlidesUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = userName,
                    Image = info.Principal.FindFirst("image").Value,
                };

                IdentityResult identResult = await userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                return Unauthorized();
            }
        }
    }
}
