using Microsoft.AspNetCore.Mvc;

namespace KartverketGroup20.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
