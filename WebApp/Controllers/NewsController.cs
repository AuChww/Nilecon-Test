using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebApp.Entity;
using WebApp.Service.Interface;

namespace WebApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _service;

        public NewsController(INewsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetAllNews()
        {
            var news = await _service.GetAllAsync();
            return View("Index", news);
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            string pattern = @"(https?:\/\/[^\s]+)";
            item.NewsDetail = Regex.Replace(item.NewsDetail, pattern,
                "<br><img src=\"$1\" class='img-fluid my-2'/><br>");

            return View("Details", item); 
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var allNews = await _service.GetAllAsync();
            if (string.IsNullOrWhiteSpace(searchTerm))
                return RedirectToAction(nameof(GetAllNews));

            var filteredNews = allNews.Where(n => 
                n.NewsName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                n.NewsDetail.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            );

            ViewBag.CurrentSearch = searchTerm;
            return View("Index", filteredNews);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm]News news, IFormFile? imageFile)
        {
            try 
            {
                if (news.Id == 0)
                    return BadRequest("Invalid news ID");

                var existing = await _service.GetByIdAsync(news.Id);
                if (existing == null) 
                    return NotFound($"News with ID {news.Id} not found");

                if (string.IsNullOrEmpty(news.NewsName) || string.IsNullOrEmpty(news.NewsDetail))
                    return BadRequest("News name and detail are required");

                existing.NewsName = news.NewsName;
                existing.NewsDetail = news.NewsDetail;

                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "news");
                    Directory.CreateDirectory(uploadsFolder);

                    var extension = Path.GetExtension(imageFile.FileName);
                    var fileName = $"NewsImage_{existing.Id}{extension}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    existing.NewsImage = fileName;
                }

                await _service.UpdateAsync(existing);
                return RedirectToAction(nameof(GetAllNews));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var news = await _service.GetByIdAsync(id);
                if (news == null)
                    return NotFound();

                if (!string.IsNullOrEmpty(news.NewsImage))
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "news", news.NewsImage);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(GetAllNews));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}