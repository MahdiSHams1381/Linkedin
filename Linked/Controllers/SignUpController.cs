using Domain;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IO;
using System.Linq;
using Linked.cardViewModul;

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
            var model = new SignUpViewModel();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (!model.Name.Any(n => Char.IsLetter(n) == true))
            {
                ModelState.AddModelError("Name", "Name Should have at least one letter !");
                return View(model);
            }
                if (await _user.IsUserExistAsync(model.Name))
            {
                ModelState.AddModelError("Name", "User name is already registered");
                return View(model);
            }

            string profileName;
            if (model.Profile == null)
            {
                profileName = "/assets/img/hero-bg.jpg";
            }
            else
            {
                string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(model.Profile.FileName);
                string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "Profile", newAvatarURL);
                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    model.Profile.CopyTo(stream);
                }
                profileName = "/assets/img/Profile/" + newAvatarURL;
            }
            User newUser = new Domain.User
            {
                DateOfBirth = model.DateOfBirth,
                Field = model.Field,
                Name = model.Name,
                Profile = profileName,
                UniversityLocation = model.UniversityLocation,
                WorkPlace = model.WorkPlace,

            };
            await _user.AddUserAsync(newUser);
            await _user.SaveChangesAsync();
            foreach (var spec in model.UserSpecialties.Split(","))
            {
                await _user.AddSpecialityToUserAsync(newUser.Id,spec);
            }
            await _user.SaveChangesAsync();
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

