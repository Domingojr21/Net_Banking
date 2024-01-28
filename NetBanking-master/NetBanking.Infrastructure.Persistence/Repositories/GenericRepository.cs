using Microsoft.EntityFrameworkCore;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Domain.Common;
using NetBanking.Infrastructure.Persistence.Context;


namespace NetBanking.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IsStatusEntity
    {
        private readonly NetBankingContext _dbContext;

        public GenericRepository(NetBankingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
           await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Set<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
            return entity;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            var entityList = await _dbContext.Set<T>().Where(e => e.Status == true).ToListAsync();
            return entityList;
        }

        public virtual async Task UpdateAsync(T entity, int id)
        {
            var entry = await GetByIdAsync(id);
           _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _dbContext.Entry(entity).Property("Status").CurrentValue = false;
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllWithIncludes(List<string> properties)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            foreach (var property in properties)
            {
                query.Include(property);
            }

            return await query.ToListAsync();
        }
    }
}
