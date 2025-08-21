using Microsoft.AspNetCore.Mvc;

namespace BuddyR.Api.Controllers
{
    public class FriendsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
