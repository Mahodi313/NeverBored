using Microsoft.EntityFrameworkCore;
using NeverBored.Models;

namespace NeverBored.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<ActivityModel> Activites { get; set; }
    }
}
