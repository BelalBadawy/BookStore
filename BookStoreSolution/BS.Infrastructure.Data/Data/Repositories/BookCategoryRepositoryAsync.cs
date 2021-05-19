using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS.Application.Interfaces.Repositories;
using BS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BS.Infrastructure.Data.Data.Repositories
{
    public class BookCategoryRepositoryAsync : Repository<BookCategory>, IBookCategoryRepositoryAsync
    {
        public BookCategoryRepositoryAsync(DbContext context) : base(context)
        {

        }
    }
}
