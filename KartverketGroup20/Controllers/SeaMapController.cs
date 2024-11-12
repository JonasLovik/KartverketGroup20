using Microsoft.AspNetCore.Mvc;

namespace KartverketGroup20.Controllers
{
    public class SeaMapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
