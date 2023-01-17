
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

using Linked.Api;
using Microsoft.Extensions.Logging;
using Services;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using Linked.cardViewModul;

namespace Linked.Controllers
{
    public class SignInController : Controller
    {
        private readonly UserRepo _user;
        public SignInController(UserRepo user)
        {
            _user = user;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await _user.GetUserByNameOrId(model.NameOrId);
            if (user != null && !string.IsNullOrWhiteSpace(user.Name))
            {
                UserAuthentication(user.Id, user.Name, model.RememberMe);
            }
            else
            {
                ModelState.AddModelError("NameOrId", "Name Or Id doesn't Exist !");
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }
        private List<Claim> UserAuthentication(int id, string userName, bool isRememberMe = false)
        {
            var claims = new List<Claim>()
            {
                new System.Security.Claims.Claim(ClaimTypes.Name,userName),
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier,id.ToString())
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principle = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = isRememberMe
            };
            HttpContext.SignInAsync(principle, properties);
            return claims;
        }
    }
}
