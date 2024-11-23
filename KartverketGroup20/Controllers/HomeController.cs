using KartverketGroup20.Data;
using KartverketGroup20.Models;
using KartverketGroup20.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Data;

namespace KartverketGroup20.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ReportService _reportService;

        private static List<Report> report = new List<Report>();


        public HomeController(ILogger<HomeController> logger, AppDbContext context,
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

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        [Authorize]
        [HttpGet]
        // GET: Henter rapporter fra db og viser alle rapportene for innlogget bruker basert på userId
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
        // GET: Henter en rapport fra db basert på ReportId og userId for visning
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
        [ValidateAntiForgeryToken]
        // POST: Oppdaterer en rapport i db basert på ReportId og userId
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
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogWarning(error.ErrorMessage);
                    }
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        // GET: Henter en rapport fra db basert på ReportId og userId for sletting
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
        // POST: Sletter en rapport fra db basert på ReportId og userId
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            _reportService.DeleteReport(id, userId);
            return RedirectToAction("UpdateOverview");
        }
    }
}
