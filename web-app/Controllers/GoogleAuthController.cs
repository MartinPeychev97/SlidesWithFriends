﻿using DAL.EntityModels.User;
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
        private UserManager<SlidesUser> userManager;
        private SignInManager<SlidesUser> signInManager;

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
            string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
            
            if (result.Succeeded)
                return View(userInfo);
            else
            {
                SlidesUser user = new SlidesUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                };

                IdentityResult identResult = await userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
                        return View(userInfo);
                    }
                }
                return Unauthorized();
            }
        }
    }
}
