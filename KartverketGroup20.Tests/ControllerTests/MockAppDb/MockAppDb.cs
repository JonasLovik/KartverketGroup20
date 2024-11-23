using KartverketGroup20.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace KartverketGroup20.Tests.ControllersTests.MockDatabase
{
    public class MockAppDb : AppDbContext
    {
        public MockAppDb(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }

    public class LandMapControllerTests
    {
        private AppDbContext _context;

        public LandMapControllerTests(DbContextOptions<AppDbContext> options)
        {
            _context = new MockAppDb(options);
        }

        public LandMapControllerTests() : this(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockAppDb")
            .Options)
        {
        }
    }
}