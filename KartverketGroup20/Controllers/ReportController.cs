using KartverketGroup20.Data;
using KartverketGroup20.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KartverketGroup20.Controllers
{
    public class ReportController : Controller
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
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

            // Hvis rapporten ikke finnes, vis en feilmelding eller 404-side
            if (report == null)
            {
                return NotFound("Rapporten ble ikke funnet.");
            }

            // Send rapporten som modellen til visningen
            return View("Detail", report);
        }

    }

}
