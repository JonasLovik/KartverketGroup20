using Microsoft.AspNetCore.Mvc;

namespace KartverketGroup20.Controllers
{
    public class LandMapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoadMap()
        {
            return View();
        }
    }
}
