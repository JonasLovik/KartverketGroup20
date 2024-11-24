using FakeItEasy;
using FluentAssertions;
using KartverketGroup20.Controllers;
using KartverketGroup20.Data;
using KartverketGroup20.Data.Enum;
using KartverketGroup20.Models;
using KartverketGroup20.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;
using WebApplication1.Data;

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
        private DbContextOptions<AppDbContext> _options;
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private Mock<ReportService> _reportServiceMock;

        public ReportControllerTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _userManagerMock = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _reportServiceMock = new Mock<ReportService>(null);
        }

        [Test]
        public void ReportController_Index_ReturnViewWithListOfReports()
        {
            using (var context = new MockAppDb(_options))
            {
                var controller = new ReportController(context, _userManagerMock.Object, _reportServiceMock.Object);

                // Arrange
                context.Reports.RemoveRange(context.Reports.Where(r => r.Id == 1 || r.Id == 2 || r.Id == 3));
                context.SaveChanges();

                var mockReports = new List<Report>
                {
                    new Report { Id = 0, Description = "Test Report 1", MapType = "Veikart", GeoJson = "{}", UserId = "1" },
                    new Report { Id = 0, Description = "Test Report 2", MapType = "Sjøkart", GeoJson = "{}", UserId = "2" }
                };

                context.Reports.AddRange(mockReports);
                context.SaveChanges();

                // Act
                var result = controller.Index();

                // Assert
                var viewResult = result as ViewResult;
                viewResult.Should().NotBeNull();
                if (viewResult != null)
                {
                    viewResult.Model.Should().BeAssignableTo<List<Report>>();

                    var model = viewResult.Model as List<Report>;
                    model.Should().NotBeNull();
                    if (model != null)
                    {
                        model.Should().HaveCount(2);
                    }
                }
            }
        }

        [Test]
        public void ReportController_ReturnsReportWithDetails()
        {
            using (var context = new MockAppDb(_options))
            {
                var controller = new ReportController(context, _userManagerMock.Object, _reportServiceMock.Object);

                // Arrange
                var mockReport = new Report
                {
                    Id = 1,
                    Description = "Test Report",
                    MapType = "Veikart",
                    GeoJson = "{}",
                    UserId = "1"
                };
                context.Reports.Add(mockReport);
                context.SaveChanges();

                var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Role, "Administrator")
                }, "mock"));

                controller.ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = user }
                };

                // Act
                var result = controller.Detail(1);

                // Assert
                var viewResult = result as ViewResult;
                viewResult.Should().NotBeNull();
                if (viewResult != null)
                {
                    viewResult.Model.Should().BeAssignableTo<Report>();

                    var model = viewResult.Model as Report;
                    model.Should().NotBeNull();
                    if (model != null)
                    {
                        model.Id.Should().Be(1);
                        model.Description.Should().Be("Test Report");
                        model.MapType.Should().Be("Veikart");
                        model.GeoJson.Should().Be("{}");
                        model.UserId.Should().Be("1");
                    }
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var context = new MockAppDb(_options))
            {
                context.Database.EnsureDeleted();
            }
        }

        public void Dispose()
        {
            using (var context = new MockAppDb(_options))
            {
                context.Dispose();
            }
        }
    }
}
