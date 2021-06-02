using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BS.API.Infrastructure;
using BS.Application.Dtos;
using BS.Application.Services.Interfaces;
using BS.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BS.API.Controllers
{
    [ApiVersion("1.0")]
    public class BookCategoryController : BaseApiController
    {
        private readonly IBookCategoryService _bookCategoryService;
        private readonly ILogger<BookCategoryController> _logger;

        public BookCategoryController(IBookCategoryService bookCategoryService, ILogger<BookCategoryController> logger)
        {
            _bookCategoryService = bookCategoryService;
            _logger = logger;
            _logger.LogInformation($"Enter the {nameof(BookCategoryController)} controller");

        }

        [Authorize(AppPermissions.AppClaim.List)]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BookCategoryReadDto>))]
        public async Task<ActionResult> GetBookCategories()
        {
            return Ok(await _bookCategoryService.GetAllAsync());
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PagedResult<BookCategoryReadDto>))]
        public async Task<ActionResult> GetBookCategoriesPagedList(int pageIndex, int pageSize)
        {
            return Ok(await _bookCategoryService.GetPagedListAsync(null, (o => o.OrderBy(x => x.DisplayOrder)), pageIndex, pageSize));
        }
        
        [HttpGet("{bookCategoryId:Guid}", Name = "GetBookCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookCategoryReadDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetBookCategoryById(Guid bookCategoryId)
        {
            var bookCategory = await _bookCategoryService.GetByIdAsync(bookCategoryId);

            if (bookCategory == null)
            {
                return NotFound();
            }

            return Ok(bookCategory);
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BookCategoryUpsertDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBookCategory(BookCategoryUpsertDto bookCategoryUpsertDto)
        {

            if (bookCategoryUpsertDto == null)
            {
                return BadRequest(ModelState);
            }

            Response<Guid> response = await _bookCategoryService.AddAsync(bookCategoryUpsertDto);

            if (response != null)
            {
                if (response.Succeeded)
                {
                    return CreatedAtRoute("GetBookCategoryById", new { bookCategoryId = response.Data }, response.Data);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            else
            {
                response = new Response<Guid>(SD.ErrorOccurred);
                return BadRequest(response);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBookCategory(BookCategoryUpsertDto bookCategoryUpsertDto)
        {

            if (bookCategoryUpsertDto == null)
            {
                return BadRequest(ModelState);
            }

            Response<bool> response = await _bookCategoryService.UpdateAsync(bookCategoryUpsertDto);

            if (response != null)
            {
                if (response.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(response);
                }
            }
            else
            {
                response = new Response<bool>(SD.ErrorOccurred);
                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBookCategory(Guid id)
        {

            if (id == Guid.Empty)
            {
                return BadRequest("id is required.");
            }

            Response<bool> response = await _bookCategoryService.DeleteAsync(id);

            if (response != null)
            {
                if (response.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(response);
                }
            }
            else
            {
                response = new Response<bool>(SD.ErrorOccurred);
                return BadRequest(response);
            }
        }


    }
}
