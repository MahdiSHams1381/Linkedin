using Linked.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Linked.Api;
using Linked.Models.User;
using System.Collections.Generic;
using Linked.cardViewModul;
namespace Linked.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int UserId)
        {
            user User_User = new api().FUNC_SearchUser(UserId);
           // List<user> LIST_UserToConection = new api().FUNC_findeThePersonConection(User_User);
            HomeCardViewModul homeCardViewModul = new HomeCardViewModul();
         //   homeCardViewModul.LIST_UserToConection = LIST_UserToConection;
            homeCardViewModul.Use_ThisUser = User_User;
            //----------------------------------------------------------------------
            List<string> d = new List<string>();
            d.Add("computerENG");
            d.Add("developer");
            d.Add("student");
            user r = new user { id = 1, name = "Mahdi Shams", universityLocation = "ssss", workplace = "ddddd", field = "fffff" , specialties = d };
            homeCardViewModul.Use_ThisUser = r;
            //----------------------------------------------------------------------
            return View(homeCardViewModul);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
