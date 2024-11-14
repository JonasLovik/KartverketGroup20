﻿using KartverketGroup20.Data;
using KartverketGroup20.Models;
using KartverketGroup20.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KartverketGroup20.Controllers
{
    public class LandMapController : Controller
    {
        //private static List<ReportViewModel> positions = new List<ReportViewModel>();

        //private static List<ReportViewModel> changes = new List<ReportViewModel>();


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

                var newReport = new Report
                {
                    GeoJson = geoJson,
                    Description = description
                };

                _context.Reports.Add(newReport);
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
            var changes_db = _context.Reports.ToList();
            return View(changes_db);
        }
        [HttpGet]
        public IActionResult TourMap()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult TourMap(string geoJson, string description)
        //{
        //    var newChange = new ReportViewModel
        //    {

        //        Id = Guid.NewGuid().ToString(),
        //        GeoJson = geoJson,
        //        Description = description
        //    };
        //    changes.Add(newChange);

        //    return View("CorrectionOverviewTourMap", changes);
        //}


    }
}

