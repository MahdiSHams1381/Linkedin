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
using System;

namespace Linked.Controllers
{
    public class HomeController : Controller
    {
        List<Queue<User>> Queue_toGetLevelToThem = new List<Queue<User>>();
        Queue<User> Queu_toLoop = new Queue<User>();
        List<User> ToAddSugg = new List<User>();
        Graph.Graph Graph_baseGraph = new Graph.Graph();
        private readonly ILogger<HomeController> _logger;
        private readonly UserRepo _User;
        public HomeController(ILogger<HomeController> logger, UserRepo User)
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
            int r = 0;
            while (ToAddSugg.Count != 10 && r < Graph_baseGraph.Getsize()) { 
                r++;
            }
            while (ToAddSugg.Count != 10  )
            {
                foreach (Queue<User> l in Queue_toGetLevelToThem)
                {
                    ToAddSugg.Add(l.Dequeue());
                }
            }
            homeCardViewModul.Suggests = ToAddSugg;

            return View(homeCardViewModul);
        }

        public  Queue<User> AddSugg(User User)
        {
            Queue<User> Queue_forReturn = new Queue<User>();
            foreach (User item1 in Graph_baseGraph.GetConnection(User.Id))
            {
                foreach (String e1 in User.Field.Split(","))
                {
                    foreach (String r1 in item1.Field.Split(","))
                    {
                        if (e1 == r1)
                        {

                            if (item1.WorkPlace == User.WorkPlace)
                            {
                                if (item1.UniversityLocation == User.UniversityLocation)
                                {
                                    Queue_forReturn.Enqueue(item1);
                                }
                            }
                            Queue_forReturn.Enqueue(item1);
                        }
                        Queue_forReturn.Enqueue(item1);
                    }
                    Queue_forReturn.Enqueue(item1);
                }   
            Queu_toLoop.Enqueue(item1);
        }
            return Queue_forReturn;
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return NotFound();
    }
}
}
