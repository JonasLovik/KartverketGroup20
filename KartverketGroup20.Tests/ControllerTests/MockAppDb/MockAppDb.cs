using KartverketGroup20.Data;
using Microsoft.EntityFrameworkCore;

namespace KartverketGroup20.Tests.ControllersTests.MockDatabase
{
    public class MockAppDb
    {
        private AppDbContext _context;

        public MockAppDb(DbContextOptions<AppDbContext> options)
        {
            _context = new AppDbContext(options);
        }

        public MockAppDb() : this(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockAppDb")
            .Options)
        {
        }
    }
}

