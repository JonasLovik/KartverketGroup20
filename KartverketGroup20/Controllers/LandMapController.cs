using KartverketGroup20.Data;
using KartverketGroup20.Models;
using KartverketGroup20.Services;
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
        private readonly ReportService _reportService;




        public LandMapController(ILogger<LandMapController> logger, AppDbContext context, 
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
                    UserId = userId,
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateOverview()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;

                var allReports = _reportService.GetAllReport(userId);
                return View(allReports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Reports in UpdateOverview.");
                return View("Error");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation($"Edit GET action called with id={id}");

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var report = _reportService.GetReportById(id, userId);
            if (report == null)
            {
                _logger.LogWarning($"Report with id={id} not found for userId={userId}");
                return NotFound();
            }

            return View(report);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(Report model)
        {
            ModelState.Remove("UserId");

            var user = await _userManager.GetUserAsync(User);

            model.UserId = user.Id;

            if (ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is valid. Updating Report.");

                _reportService.UpdateReport(model.Id, model.Description, model.GeoJson, user.Id);
                return RedirectToAction("UpdateOverview");
            }
            else
            {
                _logger.LogWarning("ModelState is invalid.");
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error  in modelState.Errors)
                    {
                        _logger.LogWarning(error.ErrorMessage);
                    }
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var report = _reportService.GetReportById(id, userId);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            _reportService.DeleteReport(id, userId);
            return RedirectToAction("UpdateOverview");
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


