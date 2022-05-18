using GraduationProject.Utilities.StaticStrings;
using GraduationProject.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GraduationProject.MvcControllers
{
    [Authorize(Roles = Roles.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInVM model)
        {
            if (!ModelState.IsValid)
                return View();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "Ahmed"),
                new Claim(ClaimTypes.Role, Roles.Admin),
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            var properties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = DateTimeOffset.Now.AddDays(14)
            };

            await HttpContext.SignInAsync(principal, properties);
            if (string.IsNullOrEmpty(model.ReturnUrl))
                return RedirectToAction(nameof(HomeController.Index), "Home");

            return LocalRedirect(model.ReturnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
    }
}
