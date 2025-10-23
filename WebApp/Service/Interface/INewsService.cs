using WebApp.Entity;

namespace WebApp.Service.Interface
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAllAsync();
        Task<News> GetByIdAsync(int id);
        Task<IEnumerable<News>> SearchAsync(string searchTerm);
        Task CreateAsync(News news);
        Task UpdateAsync(News news);
        Task DeleteAsync(int id);
    }
}
