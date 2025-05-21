using Challenge.Persistance.Base.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public void Add(TEntity entity)
        {
            lock (_tcontext)
            {
                var addedEntity = _tcontext.Entry(entity);
                addedEntity.State = EntityState.Added;
            }
        }

        public void AddRange(List<TEntity> entities)
        {
            lock (_tcontext)
            {
                _tcontext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                _tcontext.AddRange(entities);
            }
        }

        public void Delete(TEntity entity)
        {
            lock (_tcontext)
            {
                var deletedEntity = _tcontext.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
            }
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> filter)
        {
            lock (_tcontext)
            {
                return _tcontext.Set<TEntity>().FirstOrDefault(filter);
            }
        }

        public void Update(TEntity entity)
        {
            lock (_tcontext)
            {
                var updatedEntity = _tcontext.Entry(entity);
                updatedEntity.State = EntityState.Modified;
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter)
        {
            lock (_tcontext)
            {
                return filter == null
                                    ? _tcontext.Set<TEntity>().ToList()
                                    : _tcontext.Set<TEntity>().Where(filter).ToList();
            }

        }
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


        public void AddAsync(TEntity entity)
        {
            lock (_tcontext)
            {
                _tcontext.AddAsync(entity);
            }
        }

        public void SaveChanges()
        {
            lock (_tcontext)
            {
                _tcontext.SaveChanges();
            }
        }

        public void SaveChangesAsync()
        {
            try
            {
                _tcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }

        public Task DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            return null;
        }

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
 