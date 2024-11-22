using Xunit;
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
using WebApplication1.Data;

namespace KartverketGroup20.Tests.ControllersTests
{
    public class ReportControllerTests
    {
        [Fact]
        public void ReportController_Index_ShouldReturnViewWithListOfReports()
        {
            try
            {


                // Arrange
                var fakeContext = A.Fake<AppDbContext>();
                var fakeUserManager = A.Fake<UserManager<IdentityUser>>();
                var fakeReportService = A.Fake<ReportService>();

                // Mock data
                var mockReports = new List<Report>
        {
            new Report { Id = 1, Description = "Test Report 1", MapType = "Veikart" },
            new Report { Id = 2, Description = "Test Report 2", MapType = "Sjøkart" }
        };

                // Set up the mock DbContext to return the mock reports
                A.CallTo(() => fakeContext.Reports.ToList()).Returns(mockReports);

                var controller = new ReportController(fakeContext, fakeUserManager, fakeReportService);

                // Act
                var result = controller.Index();

                // Assert
                var viewResult = result as ViewResult;
                viewResult.Should().NotBeNull();
                viewResult.Model.Should().BeAssignableTo<List<Report>>();

                var model = viewResult.Model as List<Report>;
                model.Should().NotBeNull().And.HaveCount(2);
                model.Should().ContainEquivalentOf(mockReports[0]);
                model.Should().ContainEquivalentOf(mockReports[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
