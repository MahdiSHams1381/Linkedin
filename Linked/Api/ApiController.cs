using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Linked.Api
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly UserRepo _user;
        public ApiController(UserRepo user)
        {
            _user = user;

        }

        [HttpGet(nameof(GetAllSpecialties))]
        public List<Specialty> GetAllSpecialties()
        {
            return _user.GetSpecialties();
        }

        [HttpGet(nameof(SuggestSpecialty))]
        public IActionResult SuggestSpecialty(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return null;
            var result = _user.GetSpecialties(title.Split(",").LastOrDefault());
            return Ok(new { result = result , count = result.Count() });
        }

        [HttpGet(nameof(FillInput))]
        public string FillInput(string text , string title)
        {
            string result = "";
            var list = text.Split(",").SkipLast(1);
            foreach (var item in list)
            {
                result += item+',';
            }
            result += title + ',';
            return result;
        }


        [HttpGet(nameof(RemoveUserSpecialty))]
        public async Task<IActionResult> RemoveUserSpecialty(int id)
        {
            await _user.RemoveUserSpecialty(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), id);
            await _user.SaveChangesAsync();
            return RedirectToAction("Index", "Profile");
        }
    }
}
