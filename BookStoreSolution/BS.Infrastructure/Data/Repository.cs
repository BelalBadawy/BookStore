using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BS.Application.Interfaces;
using BS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BS.Infrastructure.Data
{
    public class Repository<T> : IGenericRepositoryAsync<T> where T : class
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<T> DbSet;

        public Repository(DbContext context)
        {
            DbContext = context ?? throw new ArgumentException(nameof(context));
            DbSet = DbContext.Set<T>();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await DbSet.AnyAsync(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await DbSet.CountAsync(predicate);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet;

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? (await orderBy(query).FirstOrDefaultAsync()) : (await query.FirstOrDefaultAsync());
        }

        public virtual async Task<T> GetAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet;
            query = query.AsNoTracking();

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }

        public async Task<PagedResult<T>> GetPagedListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageIndex = 0, int pageSize = 10, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet;
            query = query.AsNoTracking();

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (predicate != null) query = query.Where(predicate);

            //  return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();

            var pagedResult = new PagedResult<T>();
            pagedResult.TotalCount = await DbSet.CountAsync();
            pagedResult.FilteredTotalCount = await query.CountAsync();
            pagedResult.Data = (orderBy != null ? await orderBy(query).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync() : await query.Skip((pageIndex -1) * pageSize).Take(pageSize).ToListAsync());

            return pagedResult;
        }


        public async Task<T> AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public async Task AddAsync(IEnumerable<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            DbSet.Update(entity);
        }

        public async Task UpdateAsync(IEnumerable<T> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public async Task DeleteAsync(object id)
        {
            T entityToDelete = await DbSet.FindAsync(id);
            DbSet.Remove(entityToDelete);
        }

        public async Task DeleteAsync(T entity)
        {
            DbSet.Remove(entity);
        }

        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }


        public void Dispose()
        {
            DbContext?.Dispose();
        }


    }
}
