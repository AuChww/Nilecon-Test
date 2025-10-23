using Microsoft.EntityFrameworkCore;
using WebApp;
using WebApp.Entity;
using WebApp.Repository.Interface;

namespace WebApp.Repository.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly AppDbContext _context;

        public NewsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.News.OrderByDescending(n => n.Id).ToListAsync();
        }

        public async Task<News?> GetByIdAsync(int id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task<IEnumerable<News>> SearchAsync(string searchTerm)
        {
            var query = _context.News.AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(n => 
                    n.NewsName.Contains(searchTerm) || 
                    n.NewsDetail.Contains(searchTerm)
                );
            }

            return await query.OrderByDescending(n => n.Id).ToListAsync();
        }

        public async Task AddAsync(News news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(News news)
        {
            _context.News.Update(news);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.News.FindAsync(id);
            if (item != null)
            {
                _context.News.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
