using Microsoft.EntityFrameworkCore;
using WebApp; // หรือ Dbcontext.cs ของคุณ
using WebApp.Repository.Interface;
using WebApp.Repository.Repository;
using WebApp.Service.Interface;
using WebApp.Service.Service;
using WebApp.Seeder;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Diagnostics;



var builder = WebApplication.CreateBuilder(args);

// Add MVC
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Service / Repository
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<INewsService, NewsService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    NewsSeeder.Seed(context);
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=News}/{action=GetAllNews}/{id?}");

app.Run();
