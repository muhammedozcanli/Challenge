using Challenge.Persistance.Base.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Challenge.Persistence.Repositories
{

    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity, new()
    { 
        private readonly ChallengeDBContext _tcontext;

        public BaseRepository(ChallengeDBContext tContext)
        {
            _tcontext = tContext;
        }

        /// <summary>
        /// Adds a single entity to the context with Added state.
        /// </summary>
        public void Add(TEntity entity)
        {
            lock (_tcontext)
            {
                var addedEntity = _tcontext.Entry(entity);
                addedEntity.State = EntityState.Added;
            }
        }

        /// <summary>
        /// Adds a range of entities to the context with NoTracking behavior.
        /// </summary>
        public void AddRange(List<TEntity> entities)
        {
            lock (_tcontext)
            {
                _tcontext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                _tcontext.AddRange(entities);
            }
        }

        /// <summary>
        /// Marks a single entity as Deleted in the context.
        /// </summary>
        public void Delete(TEntity entity)
        {
            lock (_tcontext)
            {
                var deletedEntity = _tcontext.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
            }
        }

        /// <summary>
        /// Gets a single entity matching the given filter expression.
        /// </summary>
        public TEntity? Get(Expression<Func<TEntity, bool>> filter)
        {
            lock (_tcontext)
            {
                return _tcontext.Set<TEntity>().FirstOrDefault(filter);
            }
        }

        /// <summary>
        /// Marks a single entity as Modified in the context.
        /// </summary>
        public void Update(TEntity entity)
        {
            lock (_tcontext)
            {
                var updatedEntity = _tcontext.Entry(entity);
                updatedEntity.State = EntityState.Modified;
            }
        }

        /// <summary>
        /// Retrieves a list of entities matching the given filter expression. Returns all if filter is null.
        /// </summary>
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter)
        {
            lock (_tcontext)
            {
                return filter == null
                                    ? _tcontext.Set<TEntity>().ToList()
                                    : _tcontext.Set<TEntity>().Where(filter).ToList();
            }
        }

        /// <summary>
        /// Asynchronously retrieves a list of entities matching the given filter expression. Returns all if filter is null.
        /// </summary>
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter)
        {
            using (var semaphore = new SemaphoreSlim(1, 1))
            {
                await semaphore.WaitAsync();
                try
                {
                    var query = filter == null
                                    ? _tcontext.Set<TEntity>()
                                    : _tcontext.Set<TEntity>().Where(filter);
                    return await query.ToListAsync();
                }
                finally
                {
                    semaphore.Release();
                }
            }
        }

        /// <summary>
        /// Adds a single entity asynchronously to the context. Note: No await used.
        /// </summary>
        public void AddAsync(TEntity entity)
        {
            lock (_tcontext)
            {
                _tcontext.AddAsync(entity);
            }
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        public void SaveChanges()
        {
            lock (_tcontext)
            {
                _tcontext.SaveChanges();
            }
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the database.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            try
            {
                await _tcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
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
                ? _tcontext.Set<TEntity>().ToList()
                : _tcontext.Set<TEntity>().Where(filter).ToList();

            if (entities.Count != 0)
            {
                _tcontext.RemoveRange(entities);
                await _tcontext.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Asynchronously adds a list of entities to the context.
        /// </summary>
        public void AddRangeAsync(List<TEntity> entities)
        {
            lock (_tcontext)
            {
                try
                {
                    _tcontext.AddRangeAsync(entities);
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Updates a list of entities in the context with NoTracking behavior.
        /// </summary>
        public void UpdateRange(List<TEntity> entities)
        {
            lock (_tcontext)
            {
                _tcontext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                _tcontext.UpdateRange(entities);
            }
        }

    }
}
 