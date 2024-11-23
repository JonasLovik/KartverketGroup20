using FluentAssertions;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using KartverketGroup20.Controllers;
using KartverketGroup20.Models;
using KartverketGroup20.Services;
using Microsoft.AspNetCore.Identity;
using KartverketGroup20.Data;
using NUnit.Framework;
using WebApplication1.Data;
using System;
using Microsoft.EntityFrameworkCore;

namespace KartverketGroup20.Tests.ControllersTests
{
    public class MockAppDb : AppDbContext
    {
        public MockAppDb(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }

    [TestFixture]
    public class ReportControllerTests : IDisposable
    {
        private MockAppDb _context;
        private UserManager<IdentityUser> _userManager;
        private ReportService _reportService;
        private ReportController _controller;

        public ReportControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new MockAppDb(options);

            _userManager = A.Fake<UserManager<IdentityUser>>();
            _reportService = A.Fake<ReportService>();

            _controller = new ReportController(_context, _userManager, _reportService);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void ReportController_Index_ShouldReturnViewWithListOfReports()
        {
            // Arrange
            var mockReports = new List<Report>
            {
                new Report { Id = 1, Description = "Test Report 1", MapType = "Veikart", GeoJson = "{}", UserId = "1" },
                new Report { Id = 2, Description = "Test Report 2", MapType = "Sjøkart", GeoJson = "{}", UserId = "2" }
            };

            _context.Reports.AddRange(mockReports);
            _context.SaveChanges();

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            viewResult.Should().NotBeNull();
            viewResult.Model.Should().BeAssignableTo<List<Report>>();

            var model = viewResult.Model as List<Report>;
            model.Should().NotBeNull().And.HaveCount(2);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
