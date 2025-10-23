using WebApp;
using WebApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Seeder
{
    public static class NewsSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.News.Any())
            {
                context.News.AddRange(
                    new News { NewsName = "Breaking News", NewsImage = "NewsImage_1.jpg", },
                    new News { NewsName = "World Update", NewsImage = "NewsImage_2.jpg" }
                );
                context.SaveChanges();
            }
        }
    }
}
