using KartverketGroup20.Data;
using KartverketGroup20.Models;
using KartverketGroup20.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        private readonly UserManager<IdentityUser> _userManager;




        public LandMapController(ILogger<LandMapController> logger, AppDbContext context, 
                                 UserManager<IdentityUser> userManager)
                                
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RoadMap(string geoJson, string description)
        {
            try
            {
                if (string.IsNullOrEmpty(geoJson) || string.IsNullOrEmpty(description))
                {
                    return BadRequest("Invalid Data");
                }

                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;
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
        public IActionResult CorrectionOverviewRoadMap()
        {
            List<Report> report= _context.Reports.ToList();
            return View(report);
        }
        [HttpGet]
        public IActionResult TourMap()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TourMap(string geoJson, string description)
        {
            try
            {
                if (string.IsNullOrEmpty(geoJson) || string.IsNullOrEmpty(description))
                {
                    return BadRequest("Invalid Data");
                }

                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;
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

        [HttpGet]
        public IActionResult CorrectionOverviewTourMap()
        {
            List<Report> report = _context.Reports.ToList();
            return View(report);
        }
    }
}


