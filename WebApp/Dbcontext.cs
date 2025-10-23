using Microsoft.EntityFrameworkCore;
using WebApp.Entity; 

namespace WebApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<News> News { get; set; }
    }
}
