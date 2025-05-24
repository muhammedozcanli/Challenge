using Challenge.Persistance.Base.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Challenge.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity, new()
    { 
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a single entity to the context with Added state.
        /// </summary>
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Adds a range of entities to the context with NoTracking behavior.
        /// </summary>
        public void AddRange(List<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        /// <summary>
        /// Marks a single entity as Deleted in the context.
        /// </summary>
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Gets a single entity matching the given filter expression.
        /// </summary>
        public TEntity? Get(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().FirstOrDefault(filter);
        }

        /// <summary>
        /// Marks a single entity as Modified in the context.
        /// </summary>
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        /// <summary>
        /// Retrieves a list of entities matching the given filter expression. Returns all if filter is null.
        /// </summary>
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter)
        {
            return filter == null
                ? _context.Set<TEntity>().ToList()
                : _context.Set<TEntity>().Where(filter).ToList();
        }

        /// <summary>
        /// Asynchronously retrieves a list of entities matching the given filter expression. Returns all if filter is null.
        /// </summary>
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter)
        {
            var query = filter == null
                ? _context.Set<TEntity>()
                : _context.Set<TEntity>().Where(filter);
            return await query.ToListAsync();
        }

        /// <summary>
        /// Adds a single entity asynchronously to the context. Note: No await used.
        /// </summary>
        public void AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().AddAsync(entity);
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the database.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously deletes all entities that match the specified filter from the database.
        /// If the filter is null, all entities will be deleted.
        /// </summary>
        /// <param name="filter">A LINQ expression to filter the entities to be deleted.</param>
        /// <returns>A Task representing the asynchronous delete operation.</returns>
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            var entities = filter == null
                ? _context.Set<TEntity>().ToList()
                : _context.Set<TEntity>().Where(filter).ToList();

            if (entities.Count != 0)
            {
                _context.RemoveRange(entities);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Asynchronously adds a list of entities to the context.
        /// </summary>
        public void AddRangeAsync(List<TEntity> entities)
        {
            _context.Set<TEntity>().AddRangeAsync(entities);
        }

        /// <summary>
        /// Updates a list of entities in the context with NoTracking behavior.
        /// </summary>
        public void UpdateRange(List<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
        }
    }
}
 