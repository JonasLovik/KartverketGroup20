using KartverketGroup20.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace KartverketGroup20.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>//DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }

        public DbSet<Report> Reports { get; set; }
    }
    
}
