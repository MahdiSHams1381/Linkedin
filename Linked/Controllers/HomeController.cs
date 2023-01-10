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
            User User_User = new api().FUNC_SearchUser(UserId);
           // List<User> LIST_UserToConection = new api().FUNC_findeThePersonConection(User_User);
            HomeCardViewModul homeCardViewModul = new HomeCardViewModul();
         //   homeCardViewModul.LIST_UserToConection = LIST_UserToConection;
            homeCardViewModul.Use_ThisUser = User_User;
            //----------------------------------------------------------------------
            List<string> d = new List<string>();
            d.Add("computerENG");
            d.Add("developer");
            d.Add("student");
            User r = new User { Id = 1, Name = "Mahdi Shams", UniversityLocation = "ssss", WorkPlace = "ddddd", Field = "fffff" };
            homeCardViewModul.Use_ThisUser = r;
            //----------------------------------------------------------------------
            return View(homeCardViewModul);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return NotFound();
        }
    }
}
