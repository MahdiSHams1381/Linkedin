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
using Graph;
using Microsoft.AspNetCore.Routing.Internal;

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
            //get the connectionof user(0 to5 )

            //add to suggestion
            List<User> ToAddSugg = new List<User>();
            Graph.Graph g = new Graph.Graph();
           foreach(User item1 in g.GetConnection(UserId))
            {
                ToAddSugg.Add(item1);
                foreach (User item2 in GetConnection(item1.Id))
                {
                    ToAddSugg.Add(item2);
                    foreach (User item3 in GetConnection(item2.Id))
                    {
                        ToAddSugg.Add(item3);
                        foreach (User item4 in GetConnection(item2.Id))
                        {
                            ToAddSugg.Add(item4);
                            foreach (User item5 in GetConnection(item4.Id))
                            {
                                ToAddSugg.Add(item5);
                                foreach (User item6 in GetConnection(item5.Id))
                                {
                                    ToAddSugg.Add(item6);
                                }
                            }
                        }
                    }
                }
            }
            homeCardViewModul.Suggests = ToAddSugg;
            return View(homeCardViewModul);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return NotFound();
        }
    }
}
