using FakeItEasy;
using FluentAssertions;
using KartverketGroup20.Controllers;
using KartverketGroup20.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace KartverketGroup20.Tests.ControllersTests
{
    [TestFixture]
    public class LandMapControllerTests : IDisposable
    {
        private LandMapController _landMapController;
        private readonly ReportService _reportService;
        private readonly UserManager<IdentityUser> _userManager;
        private AppDbContext _context;
        private DbContextOptions<AppDbContext> _options;

        public LandMapControllerTests()
        {
            //Dependencies
            _reportService = A.Fake<ReportService>();
            _userManager = A.Fake<UserManager<IdentityUser>>();
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(_options); // Use _options to initialize _context

            //SUT
            _landMapController = new LandMapController(_context, _userManager, _reportService);
        }

        [SetUp]
        public void Setup()
        {
            // Initialize any required data here
        }

        [Test]
        public void LandMapController_Index_ReturnsViewResult()
        {
            //Arrange
            var controller = new LandMapController(_context, _userManager, _reportService);

            //Act
            var result = _landMapController.Index();

            //Assert
            result.Should().BeOfType<ViewResult>();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _landMapController?.Dispose();
            _context?.Dispose();
            (_userManager as IDisposable)?.Dispose();
        }
    }
}
