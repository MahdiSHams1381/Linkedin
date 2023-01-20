using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Linked.Controllers
{
    public class Utility : Controller
    {
        public Task<int> GetAuthenticatedUser()
        {
            return Task.FromResult(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
    }
}
