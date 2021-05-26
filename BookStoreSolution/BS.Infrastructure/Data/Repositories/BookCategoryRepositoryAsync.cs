using BS.Application.Interfaces.Repositories;
using BS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BS.Infrastructure.Data.Repositories
{
    public class BookCategoryRepositoryAsync : Repository<BookCategory>, IBookCategoryRepositoryAsync
    {
        public BookCategoryRepositoryAsync(DbContext context) : base(context)
        {

        }
    }
}
