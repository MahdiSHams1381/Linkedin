
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

using Linked.Api;
using Microsoft.Extensions.Logging;
using Services;
using System.Threading.Tasks;
using Domain;

namespace Linked.Controllers
{
    public class SignInController : Controller
    {
        private readonly UserRepo _user;
        public SignInController(UserRepo user)
        {
            _user = user;
        }

        public async Task Addperson(int INT_Id__IdForEachUser__theAnalysesValue, DateTime DATE_YearOfBirth, string STR_FirstName__FirstNameOFUser, string STR_WorkPlace__ThePlaceOfUserWork, string Str_Field__TheFieldOfStudy, string STR_University__ThePlaceOfUnivesity)
        {
            User User_NewUser = new User { Id = INT_Id__IdForEachUser__theAnalysesValue, Name = STR_FirstName__FirstNameOFUser, Field = Str_Field__TheFieldOfStudy, UniversityLocation = STR_University__ThePlaceOfUnivesity, WorkPlace = STR_WorkPlace__ThePlaceOfUserWork };
            await _user.AddUserAsync(User_NewUser);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
