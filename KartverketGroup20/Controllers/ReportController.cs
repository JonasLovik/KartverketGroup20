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
            Report report = _context.Reports.FirstOrDefault(r => r.Id == id);
            return View(report);
        }
    }

}
