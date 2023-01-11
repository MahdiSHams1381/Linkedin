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
            //var temp = new ReadData(_User);
            //await temp.ReadDataFromJsonAsync();
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "SignIn");
            HomeCardViewModul homeCardViewModul = new HomeCardViewModul();
            homeCardViewModul.Use_ThisUser = await _User.GetUserAsync(1);
            
            return View(homeCardViewModul);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return NotFound();
        }
    }
}
