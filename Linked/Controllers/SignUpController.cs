using Domain;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IO;

namespace Linked.Controllers
{
    public class SignUpController : Controller
    {
        private readonly UserRepo _user;
        public SignUpController(UserRepo user)
        {
            _user = user;

        }

        public IActionResult Index()
        {
            return View(new User());
        }
        [HttpPost]
        public async Task<IActionResult> Index(User model,IFormFile Profile)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (await _user.IsUserExistAsync(model))
            {
                ModelState.AddModelError("Name", "User name is already registered");
                return View(model);
            }
            if (Profile == null)
            {
                model.Profile = "/assets/img/Defult.jpg";
            }
            else
            {
                string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(Profile.FileName);
                string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "Profile", newAvatarURL);
                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    Profile.CopyTo(stream);
                }
                model.Profile = newAvatarURL;
            }
            await _user.AddUserAsync(model);
            return RedirectToAction("Index", "SignIn");
        }

        #region Logout
        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}

