using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BS.Application.Dtos;
using BS.Domain.Common;
using BS.Domain.Entities;

namespace BS.Application.Services.Interfaces
{
    public partial interface IBookCategoryService
    {
        Task<Response<Guid>> AddAsync(BookCategoryUpsertDto bookCategoryUpsertDto);
        Task<Response<bool>> UpdateAsync(BookCategoryUpsertDto bookCategoryUpsertDto);
        Task<Response<bool>> DeleteAsync(Guid id);
        Task<Response<BookCategoryReadDto>> GetByIdAsync(Guid id);
        Task<Response<List<BookCategoryReadDto>>> GetAllAsync(Expression<Func<BookCategory, bool>> predicate = null,
            Func<IQueryable<BookCategory>, IOrderedQueryable<BookCategory>> orderBy = null, params Expression<Func<BookCategory, object>>[] includes);

        Task<Response<PagedResult<BookCategoryReadDto>>> GetPagedListAsync(Expression<Func<BookCategory, bool>> predicate = null,
            Func<IQueryable<BookCategory>, IOrderedQueryable<BookCategory>> orderBy = null, int pageIndex = 0,
            int pageSize = 10, params Expression<Func<BookCategory, object>>[] includes);
    }
}
