using Microsoft.EntityFrameworkCore;
using Tech.Assessment.Repository.DBContext;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Tech.Assessment.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int pageNumber = 0, int pageSize = 0);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);
        Task<T> CreateAsync<T>(T entity) where T : class, new();
        Task<TEntity> UpdateAsync(TEntity Entity);


    }
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected readonly ApplicationDBContext _defaultContext;
        public BaseRepository(ApplicationDBContext defaultContext)
        {
            _defaultContext = defaultContext;
        }
        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int pageNumber = 0, int pageSize = 0)
        {
            IQueryable<TEntity> query = _defaultContext.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            if (pageNumber > 0)
                query = query.Skip(pageNumber - 1);

            if (pageSize > 0)
                query = query.Take(pageSize);

            if (orderBy != null)
                query = orderBy(query);

            var entityList = await query.ToListAsync();

            return entityList;
        }
     
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _defaultContext.Set<TEntity>();

            if (includes != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (filter != null)
                query = query.Where(filter);

            var entity = await query.FirstOrDefaultAsync();

            return entity;
        }
        public async Task<T> CreateAsync<T>(T entity) where T : class, new()
        {
            await _defaultContext.AddAsync(entity);
            await _defaultContext.SaveChangesAsync();
            return entity;
        }
        public async Task<TEntity> UpdateAsync(TEntity Entity)
        {
            _defaultContext.Attach<TEntity>(Entity);
            await _defaultContext.SaveChangesAsync();
            return Entity;
        }

    }
}
