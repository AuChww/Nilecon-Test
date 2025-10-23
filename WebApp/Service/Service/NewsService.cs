using WebApp.Entity;
using WebApp.Service.Interface;
using WebApp.Repository.Interface;

namespace WebApp.Service.Service
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repository;

        public NewsService(INewsRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<News>> GetAllAsync() => _repository.GetAllAsync();

        public Task<News?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<IEnumerable<News>> SearchAsync(string searchTerm) => _repository.SearchAsync(searchTerm);

        public Task CreateAsync(News news) => _repository.AddAsync(news);

        public Task UpdateAsync(News news) => _repository.UpdateAsync(news);

        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
