using Linked.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Linked.Models.User;
using Linked.Api;
namespace Linked.Controllers.SinIn
{
    public class SinInController : Controller
    {
        public void Addperson(int INT_Id__IdForEachUser__theAnalysesValue ,  DateTime DATE_YearOfBirth  ,  string STR_FirstName__FirstNameOFUser  , string STR_WorkPlace__ThePlaceOfUserWork , string Str_Field__TheFieldOfStudy , string STR_University__ThePlaceOfUnivesity )
        {
            user User_NewUser = new user { id = INT_Id__IdForEachUser__theAnalysesValue, name = STR_FirstName__FirstNameOFUser, field = Str_Field__TheFieldOfStudy, universityLocation = STR_University__ThePlaceOfUnivesity, workplace = STR_WorkPlace__ThePlaceOfUserWork };
            new api().FUNC_AddPersonToDataBase(User_NewUser);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
