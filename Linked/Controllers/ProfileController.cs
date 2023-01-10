using Microsoft.AspNetCore.Mvc;

using Linked.Api;
using Domain;

namespace Linked.Controllers.Profile
{
    public class ProfileController : Controller
    {
        public IActionResult Index(int UserId)
        {
            User User_UserToPassDataFromProfile = new api().FUNC_SearchUser(UserId);
            return View(User_UserToPassDataFromProfile);
        }
    }
}
