using Microsoft.AspNetCore.Mvc;
using Linked.Models.User;
using Linked.Api;
namespace Linked.Controllers.Profile
{
    public class ProfileController : Controller
    {
        public IActionResult Index(int UserId)
        {
            user User_UserToPassDataFromProfile = new api().FUNC_SearchUser(UserId);
            return View(User_UserToPassDataFromProfile);
        }
    }
}
