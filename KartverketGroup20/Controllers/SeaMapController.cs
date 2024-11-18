using KartverketGroup20.Data;
using KartverketGroup20.Models;
using KartverketGroup20.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using WebApplication1.Data;

namespace KartverketGroup20.Controllers
{
    public class SeaMapController : Controller
    {
        //private static List<ReportViewModel> positions = new List<ReportViewModel>();

        private static List<Report> report = new List<Report>();

        private readonly ILogger _logger;

        private readonly AppDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ReportService _reportService;


        public SeaMapController(ILogger<SeaMapController> logger, AppDbContext context, 
                                UserManager<IdentityUser> userManager, ReportService reportService)

        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _reportService = reportService;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SeaMap(string geoJson, string description)
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
                    UserId = userId,
                    GeoJson = geoJson,
                    Description = description,
                    ReportTime = DateTime.Now,
                    MapType = "Sjøkart"
                };

                _context.Reports.Add(report);
                _context.SaveChanges();

                return View("CorrectionOverview", new List<Report> { report });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}, Inner Exeption: {ex.InnerException?.Message}");
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CorrectionOverviewRoadMap()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var report = _reportService.GetAllReport(userId);
            //List<Report> report= _context.Reports.ToList();
            return View(report);
        }
    }


}