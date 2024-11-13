using KartverketGroup20.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace KartverketGroup20.Controllers
{
    public class LandMapController : Controller
    {
        private static List<ReportViewModel> positions = new List<ReportViewModel>();

        private static List<ReportViewModel> changes = new List<ReportViewModel>();
   
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RoadMap()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RoadMap(ReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                changes.Add(model);
                return View("CorrectionOverview", changes);
            }
            return View();
        }
        [HttpGet]
        public IActionResult TourMap()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TourMap(ReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                changes.Add(model);
                return View("CorrectionOverviewTourMap", changes);
            }
            return View();
        }
        //[HttpGet]
        ////public IActionResult CorrectionOverview(string GeoJson, string description)
        //{
        //    var newChange = new ReportViewModel
        //    {
        //        //Id = Guid.NewGuid().ToString(),
        //        //GeoJson = geoJson,
        //        Description = description
        //    };
        //    changes.Add(newChange);

        //}
    }

  
}
