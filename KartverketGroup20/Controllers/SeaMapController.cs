using KartverketGroup20.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace KartverketGroup20.Controllers
{
    public class SeaMapController : Controller
    {
        //private static List<ReportViewModel> positions = new List<ReportViewModel>();

        private static List<ReportViewModel> changes = new List<ReportViewModel>();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SeaMap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SeaMap(string geoJson, string description)
        {
            var newChange = new ReportViewModel
            {
                Id = Guid.NewGuid().ToString(),
                GeoJson = geoJson,
                Description = description
            };
            changes.Add(newChange);

            return View("CorrectionOverview", changes);
        }

        [HttpGet]
        public IActionResult CorrectionOverview()
        {
            return View(changes);
        }
    }


}