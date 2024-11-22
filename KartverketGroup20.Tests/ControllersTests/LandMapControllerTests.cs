using FakeItEasy;
using FluentAssertions;
using KartverketGroup20.Controllers;
using KartverketGroup20.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data;
using Xunit;

namespace KartverketGroup20.Tests.ControllersTests
{
    public class LandMapControllerTests
    {
        private readonly LandMapController _landMapController;
        private ReportService _reportService;
        private UserManager<IdentityUser> _userManager;
        private AppDbContext _context;
        public LandMapControllerTests()
        {
            //Dependencies
            _reportService = A.Fake<ReportService>();
            _userManager = A.Fake<UserManager<IdentityUser>>();
            _context = A.Fake<AppDbContext>();

            //SUT
            _landMapController = new LandMapController(_context, _userManager, _reportService);
        }

        [Fact]
        public void LandMapController_Index_ReturnsSuccess()
        {
            //Arrange

            //Act
            var result = _landMapController.Index();
            //Assert
            result.Should().BeOfType<IActionResult>();
        }
    }
}
