using KartverketGroup20.Data;
using KartverketGroup20.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace KartverketGroup20.Controllers
{
    public class SeaMapController : Controller
    {
        //private static List<ReportViewModel> positions = new List<ReportViewModel>();

        private static List<Report> report = new List<Report>();

        private readonly ILogger _logger;

        private readonly AppDbContext _context;

        public SeaMapController(ILogger<SeaMapController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

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
            List<Report> report = _context.Reports.ToList();
            return View(report);
        }
    }


}