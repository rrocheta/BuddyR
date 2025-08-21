using Microsoft.AspNetCore.Mvc;

namespace BuddyR.Api.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
