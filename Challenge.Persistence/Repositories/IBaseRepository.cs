using Challenge.Persistance.Base.Entities;
using System.Linq.Expressions;

namespace Challenge.Persistence.Repositories
{
    public interface IBaseRepository<T>
         where T : BaseEntity, new()
    {
        public T? Get(Expression<Func<T, bool>> filter);
        public List<T> GetList(Expression<Func<T, bool>>? filter = null);
        public Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter);
        public void Add(T entity);
        public void AddAsync(T entity);
        public void AddRange(List<T> entities);
        public void UpdateRange(List<T> entities);
        public void Update(T entity);
        public void Delete(T entity);
        public void SaveChanges();
        public Task SaveChangesAsync();
        public Task DeleteAsync(Expression<Func<T, bool>> filter);
        public void AddRangeAsync(List<T> entities);
    }
}
