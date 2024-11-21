using KartverketGroup20.Data;
using KartverketGroup20.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace KartverketGroup20.Controllers
{
    public class ReportController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly AppDbContext _context;
        private readonly ReportService _reportService;

        public ReportController(AppDbContext context, UserManager<IdentityUser> userManager,
            ReportService reportService)
        {
            _context = context;
            _userManager = userManager;

            _reportService = reportService;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            var reports = _context.Reports.ToList();
            return View(reports);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Detail(int id)
        {
            // Hent rapporten basert på ID
            Report report = _context.Reports.FirstOrDefault(r => r.Id == id);

            // Hvis rapporten ikke finnes vis en feilmelding
            if (report == null)
            {
                return NotFound("Rapporten ble ikke funnet.");
            }

            // Send rapporten til view
            return View("Detail", report);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditDetail(Report model)
        {
            ModelState.Remove("UserId");

            var user = await _userManager.GetUserAsync(User);
            model.UserId = user.Id;

            if (ModelState.IsValid)
            {


                _reportService.UpdateReportAdmin(model.Id, user.Id, model.Feedback, model.Status);
                return RedirectToAction("Index");
            }
            else
            {

                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        return View(error);
                    }
                }
            }
            return View(model);
        }

    }

}
