using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreStore.Infrastructure.Data;
using BS.API.Infrastructure;
using BS.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BS.API.Controllers
{
   
    public class BookCategoryController : BaseApiController
    {
        private readonly IBookCategoryService _bookCategoryService;
        public BookCategoryController(DbContext context, IBookCategoryService bookCategoryService)
        {

            _bookCategoryService = bookCategoryService;
        }
    }
}
