using FluentAssertions;
using KartverketGroup20.Controllers;
using KartverketGroup20.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using Moq;
using KartverketGroup20.Models;

namespace KartverketGroup20.Tests.ControllersTests
{
    [TestFixture]
    public class LandMapControllerTests : IDisposable
    {
        private LandMapController _controller;
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private Mock<ReportService> _mockReportService;
        private AppDbContext _context;
        private DbContextOptions<AppDbContext> _options;

        public LandMapControllerTests()
        {
            // Dependencies
            _mockReportService = new Mock<ReportService>(MockBehavior.Strict, new object[] { null });
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(),
                null, null, null, null, null, null, null, null);
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(_options);

            // SUT
            _controller = new LandMapController(_context, _mockUserManager.Object, _mockReportService.Object);
        }

        [SetUp]
        public void Setup()
        {
            // Initialize any required data here
        }

        [Test]
        public void LandMapController_Index_ReturnsViewResult()
        {
            // Act
            var result = _controller.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Test]
        public async Task RoadMap_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var geoJson = ""; // Invalid geoJson
            var description = ""; // Invalid description
            var user = new IdentityUser { Id = "test-user-id" };
            _mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).ReturnsAsync(user);

            // Act
            var result = await _controller.RoadMap(geoJson, description) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.That(result.Value, Is.EqualTo("Invalid Data"));
        }

        [Test]
        public async Task RoadMap_ValidData_ReturnsViewWithReport()
        {
            // Arrange
            var geoJson = "{\"type\":\"FeatureCollection\",\"features\":[]}";
            var description = "Test description";
            var user = new IdentityUser { Id = "test-user-id" };
            _mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).ReturnsAsync(user);

            // Act
            var result = await _controller.RoadMap(geoJson, description) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ViewName, Is.EqualTo("CorrectionOverview"));
            var model = result.Model as List<Report>;
            Assert.IsNotNull(model);
            Assert.That(model.Count, Is.EqualTo(1));
            var report = model[0];
            Assert.That(report.UserId, Is.EqualTo("test-user-id"));
            Assert.That(report.GeoJson, Is.EqualTo(geoJson));
            Assert.That(report.Description, Is.EqualTo(description));
            Assert.That(report.MapType, Is.EqualTo("Veikart"));
            Assert.That(report.Status, Is.EqualTo(Data.Enum.Status.IkkeBehandlet));
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _controller?.Dispose();
            _context?.Dispose();
            (_mockUserManager as IDisposable)?.Dispose();
        }
    }
}
