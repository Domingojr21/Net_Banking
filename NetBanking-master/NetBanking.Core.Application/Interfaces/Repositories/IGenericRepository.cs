

namespace NetBanking.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T entity, int id);
        Task DeleteAsync(int id);
        Task<List<T>> GetAllWithIncludes(List<string> properties);
     
    }
}
