using WebApp.Entity;

namespace WebApp.Repository.Interface
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllAsync();
        Task<News?> GetByIdAsync(int id);
        Task<IEnumerable<News>> SearchAsync(string searchTerm);
        Task AddAsync(News news);
        Task UpdateAsync(News news);
        Task DeleteAsync(int id);
    }
}
