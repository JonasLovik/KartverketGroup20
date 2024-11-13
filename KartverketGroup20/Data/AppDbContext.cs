using KartverketGroup20.Models;
using Microsoft.EntityFrameworkCore;


namespace KartverketGroup20.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }

        public DbSet<Report> Reports { get; set; }
    }
    
}
