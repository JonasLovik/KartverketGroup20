using KartverketGroup20.Data;
using KartverketGroup20.Models;
using KartverketGroup20.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KartverketGroup20.Controllers
{
    public class LandMapController : Controller
    {
        //private static List<ReportViewModel> positions = new List<ReportViewModel>();

        //private static List<ReportViewModel> changes = new List<ReportViewModel>();

        private static List<Report> report = new List<Report>();


        private readonly ILogger<LandMapController> _logger;

        private readonly AppDbContext _context;

        public LandMapController(ILogger<LandMapController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

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
        public IActionResult RoadMap(string geoJson, string description)
        {
            try
            {
                if (string.IsNullOrEmpty(geoJson) || string.IsNullOrEmpty(description))
                {
                    return BadRequest("Invalid Data");
                }

                var report = new Report
                {
                    GeoJson = geoJson,
                    Description = description,
                    ReportTime = DateTime.Now
                };

                _context.Reports.Add(report);
                _context.SaveChanges();

                return View("CorrectionOverview");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}, Inner Exeption: {ex.InnerException?.Message}");
                throw;
            }
        }

        [HttpGet]
        public IActionResult CorrectionOverview()
        {
            List<Report> report= _context.Reports.ToList();
            return View(report);
        }
        [HttpGet]
        public IActionResult TourMap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TourMap(string geoJson, string description)
        {
            try
            {
                if (string.IsNullOrEmpty(geoJson) || string.IsNullOrEmpty(description))
                {
                    return BadRequest("Invalid Data");
                }

                var report = new Report
                {
                    GeoJson = geoJson,
                    Description = description,
                    ReportTime = DateTime.Now
                };

                _context.Reports.Add(report);
                _context.SaveChanges();

                return View("CorrectionOverviewTourMap");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}, Inner Exeption: {ex.InnerException?.Message}");
                throw;
            }
        }
        public IActionResult CorrectionOverviewRoadMap()
        {
            List<Report> report = _context.Reports.ToList();
            return View(report);
        }
    }
}


