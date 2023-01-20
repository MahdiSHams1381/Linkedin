
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

            //--------------------------------------------
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "SignIn");
            HomeCardViewModel homeCardViewModul = new HomeCardViewModel();
            homeCardViewModul.User = await _User.GetUserAsync(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            ViewData["img"] = homeCardViewModul.User.Profile;
            //---------------------------------------------------------------------------------------------------------------
            //List<Domain.SpecialtyUser> a = new List<SpecialtyUser>();
            //a.Add(new Domain.SpecialtyUser() { Id = 3, UserId = 3 });
            //a.Add(new Domain.SpecialtyUser() { Id = 2, UserId = 2 });
            //Graph_baseGraph.Add(new User() { Id = 1, Name = "ali", UniversityLocation = "esfahan", DateOfBirth= "1598" , Profile = "lkkkkk", WorkPlace = "rasht", UserSpecialties = a, Field = "aaa , bbb" });
            //a = new List<SpecialtyUser>();
            //a.Add(new Domain.SpecialtyUser() { Id = 3, UserId = 3 });
            //a.Add(new Domain.SpecialtyUser() { Id = 4, UserId = 4 });
            //a.Add(new Domain.SpecialtyUser() { Id = 5, UserId = 5 });
            //Graph_baseGraph.Add(new User() { Id = 2, Name = "mohammad", UniversityLocation = "tehren", DateOfBirth = "1598", Profile = "lkkkkk",  WorkPlace = "kashan", UserSpecialties = a, Field = "ccc , ddd" });
            //Graph_baseGraph.Add(new User() { Id = 3, Name = "kazem", UniversityLocation = "esfahan", DateOfBirth = "1598", Profile = "lkkkkk", WorkPlace = "rasht", Field = "aaa , ddd" });
            //a = new List<SpecialtyUser>();
            //a.Add(new Domain.SpecialtyUser() { Id = 7, UserId = 7 });
            //a.Add(new Domain.SpecialtyUser() { Id = 6, UserId = 6 });
            //Graph_baseGraph.Add(new User() { Id = 4, Name = "sadegh", UniversityLocation = "esfahan", DateOfBirth = "1598", Profile = "lkkkkk", WorkPlace = "rasht", UserSpecialties = a, Field = "ccc , aaa" });
            //Graph_baseGraph.Add(new User() { Id = 5, Name = "naser", UniversityLocation = "esfahan", WorkPlace = "kashan", DateOfBirth = "1598", Profile = "lkkkkk", Field = "bbb , aaa" });
            //a = new List<SpecialtyUser>();
            //a.Add(new Domain.SpecialtyUser() { Id = 5, UserId = 5 });
            //Graph_baseGraph.Add(new User() { Id = 6, Name = "saeed", DateOfBirth = "1598", Profile = "lkkkkk", UniversityLocation = "esfahan", WorkPlace = "kashan", UserSpecialties = a, Field = "ddd , ddd" });
            //Graph_baseGraph.Add(new User() { Id = 7, Name = "ali", UniversityLocation = "esfahan", DateOfBirth = "1598", Profile = "lkkkkk", WorkPlace = "rasht", Field = "ccc , aaa" });
            //----------------------------------------------------------------------------------------------------------------
            //AddSugg(await _User.GetUserAsync(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))));
            for (int q = 0; q < 999; q = q++)
            {
                var y = _User.GetUserByNameOrId(Convert.ToString(q)).Result;
                Graph_baseGraph.Add(y);
            }
            AddSugg(Graph_baseGraph.FindElementById(1));
            while (Queue_toGetLevelToThem.Count < 10 && Queu_toLoop.Count > 0)
            {

                Queue_toGetLevelToThem.Add(AddSugg(Queu_toLoop.Dequeue()));
            }
            int ru = 0;
            while (ToAddSugg.Count != 10 && ru < 20)
            {
                foreach (Queue<User> l in Queue_toGetLevelToThem)
                {
                    bool r = true;
                    if (l.Count != 0)
                    {
                        User? g = l.Dequeue();
                        foreach (User o in ToAddSugg)
                        {
                            if (o == g)
                            {
                                r = false;
                            }
                        }
                        if (r && g != await _User.GetUserAsync(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))))
                            ToAddSugg.Add(g);
                    }
                    if (l.Count == 0)
                    {
                        ru++;
                    }
                }
            }
            homeCardViewModul.Suggests = ToAddSugg;

            return View(homeCardViewModul);
        }

        public Queue<User> AddSugg(User User)
        {
            bool d = false;
            Queue<User> Queue_forReturn = new Queue<User>();
            foreach (User item1 in Graph_baseGraph.GetConnection(User.Id))
            {
                d = false;
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
                                    if (d == false)
                                    {
                                        Queue_forReturn.Enqueue(item1);
                                        d = true;
                                    }
                                }
                            }
                            if (d == false)
                            {
                                Queue_forReturn.Enqueue(item1);
                                d = true;
                            }
                        }
                        if (d == false)
                        {
                            Queue_forReturn.Enqueue(item1);
                            d = true;
                        }
                    }
                    if (d == false)
                    {
                        Queue_forReturn.Enqueue(item1);
                        d = true;
                    }
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
