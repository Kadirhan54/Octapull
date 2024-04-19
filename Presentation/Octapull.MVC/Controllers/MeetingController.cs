using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Octapull.MVC.Controllers
{
    [Authorize]
    public class MeetingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
