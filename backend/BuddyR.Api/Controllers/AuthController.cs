using Microsoft.AspNetCore.Mvc;

namespace BuddyR.Api.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
