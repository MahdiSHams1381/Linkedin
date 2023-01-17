using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Linked.Api;
using System.Collections.Generic;
using Linked.cardViewModul;
using Services;
using DataBase;
using System.Threading.Tasks;
using Domain;
using System.Security.Claims;

namespace Linked.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserRepo _User;
        public HomeController(ILogger<HomeController> logger , UserRepo User)
        {
            _logger = logger;
            _User = User;
        }

        public async Task<IActionResult> Index(int UserId)
        {
            #region Add Json To Database
            //var temp = new ReadData(_User);
            //await temp.ReadDataFromJsonAsync();
            #endregion

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "SignIn");
            HomeCardViewModel homeCardViewModul = new HomeCardViewModel();
            homeCardViewModul.User = await _User.GetUserAsync(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            ViewData["img"] = homeCardViewModul.User.Profile;
            return View(homeCardViewModul);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return NotFound();
        }
    }
}
