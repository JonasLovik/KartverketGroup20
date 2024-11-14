using KartverketGroup20.Data;
using Microsoft.AspNetCore.Mvc;

namespace KartverketGroup20.Controllers
{
    public class ReportController : Controller
    {
    
        public IActionResult Index()
        {
            return View();
        }
    }
}
