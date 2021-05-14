using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BS.Application.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        #region Async

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);

        //Task<IPaginate<T>> GetListAsync(
        //    Expression<Func<T, bool>> predicate = null,
        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //    int index = 0,
        //    int size = 10,
        //    params Expression<Func<T, object>>[] includes);

        Task<T> GetAsync(object id);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);

        #region Add Functions

        Task<T> AddAsync(T entity);

        Task AddAsync(IEnumerable<T> entities);

        #endregion

        #region Update Functions

        Task UpdateAsync(T entity);
        Task UpdateAsync(IEnumerable<T> entities);

        #endregion

        #region Delete Functions
        Task DeleteAsync(object id);
        Task DeleteAsync(T entity);
        Task DeleteAsync(IEnumerable<T> entities);

        #endregion
        #endregion

    }
}
