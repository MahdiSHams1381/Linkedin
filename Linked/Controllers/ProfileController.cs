using Microsoft.AspNetCore.Mvc;

using Linked.Api;
using Domain;
using Services;
using System.Security.Claims;
using System.Threading.Tasks;
using Linked.cardViewModul;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Linked.Controllers.Profile
{
    public class ProfileController : Controller
    {
        private readonly UserRepo _user;
        public ProfileController(UserRepo user)
        {
            _user = user;

        }
        public async Task<IActionResult> Index()
        {
            var user = await _user.GetUserAsync(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (user == null)
                return NotFound();
            ProfileViewModel model = new ProfileViewModel()
            {
                DateOfBirth = user.DateOfBirth,
                Field = user.Field,
                Id = user.Id,
                Name = user.Name,
                UniversityLocation = user.UniversityLocation,
                WorkPlace = user.WorkPlace,
                OldProfile = user.Profile,
                OldSpecialty = user.UserSpecialties,
                
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            if (model.Id == 0)
                return RedirectToAction("Error", "Home");
            if (!ModelState.IsValid)
                return View(model);
            if (!model.Name.Any(n => Char.IsLetter(n) == true))
            {
                ModelState.AddModelError("Name", "Name Should have at least one letter !");
                return View(model);
            }
            string profileName = model.OldProfile;
            if (model.Profile != null)
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
                Id = model.Id,
            };
            await _user.UpdateUserAsync(newUser);
            await _user.SaveChangesAsync();
            if(model.Specialty!=null)
            foreach (var spec in model.Specialty.Split(","))
            {
                await _user.AddSpecialityToUserAsync(newUser.Id, spec);
            }
            await _user.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
