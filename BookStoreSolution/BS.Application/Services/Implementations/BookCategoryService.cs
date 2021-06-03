using AutoMapper;
using BS.Application.Dtos;
using BS.Application.Interfaces;
using BS.Application.Interfaces.Repositories;
using BS.Application.Services.Interfaces;
using BS.Application.Validations;
using BS.Domain.Common;
using BS.Domain.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BS.Application.Services.Implementations
{
    public partial class BookCategoryService : IBookCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPermissionChecker _permissionChecker;

        public BookCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IPermissionChecker permissionChecker)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _permissionChecker = permissionChecker;
        }

        public async Task<Response<Guid>> AddAsync(BookCategoryUpsertDto bookCategoryUpsertDto)
        {
            if (_permissionChecker.HasClaim(AppPermissions.BookCategory.Create))
            {
                BookCategoryUpsertDtoValidator dtoValidator = new BookCategoryUpsertDtoValidator();

                ValidationResult validationResult = dtoValidator.Validate(bookCategoryUpsertDto);

                if (validationResult != null && validationResult.IsValid == false)
                {
                    return new Response<Guid>(validationResult.Errors.Select(modelError => modelError.ErrorMessage)
                        .ToList());
                }
                else
                {
                    if (await _unitOfWork.Repository<IBookCategoryRepositoryAsync>()
                        .AnyAsync(o => o.Title.ToUpper() == bookCategoryUpsertDto.Title.ToUpper()))
                    {
                        return new Response<Guid>(string.Format(SD.ExistData, bookCategoryUpsertDto.Title));
                    }
                    else
                    {
                        BookCategory bookCategory = _mapper.Map<BookCategory>(bookCategoryUpsertDto);

                        var addedEntity = await _unitOfWork.Repository<IBookCategoryRepositoryAsync>()
                            .AddAsync(bookCategory);

                        int effectedRows = await _unitOfWork.CommitAsync();
                        if (effectedRows != 0)
                        {
                            return new Response<Guid>(addedEntity.Id);
                        }
                    }
                }

                return new Response<Guid>(SD.ErrorOccurred);
            }

            return new Response<Guid>("not authorized");
        }

        public async Task<Response<bool>> UpdateAsync(BookCategoryUpsertDto bookCategoryUpsertDto)
        {
            if (_permissionChecker.HasClaim(AppPermissions.BookCategory.Edit))
            {
                BookCategoryUpsertDtoValidator dtoValidator = new BookCategoryUpsertDtoValidator();

                ValidationResult validationResult = dtoValidator.Validate(bookCategoryUpsertDto);

                if (validationResult != null && validationResult.IsValid == false)
                {
                    return new Response<bool>(validationResult.Errors.Select(modelError => modelError.ErrorMessage).ToList());
                }
                else
                {
                    if (await _unitOfWork.Repository<IBookCategoryRepositoryAsync>().AnyAsync(o => o.Title.ToUpper() == bookCategoryUpsertDto.Title.ToUpper() && o.Id != bookCategoryUpsertDto.Id))
                    {
                        return new Response<bool>(string.Format(SD.ExistData, bookCategoryUpsertDto.Title));
                    }
                    else
                    {
                        var entityToUpdate = await _unitOfWork.Repository<IBookCategoryRepositoryAsync>().FirstOrDefaultAsync(x => x.Id == bookCategoryUpsertDto.Id);

                        _mapper.Map(bookCategoryUpsertDto, entityToUpdate);

                        await _unitOfWork.Repository<IBookCategoryRepositoryAsync>().UpdateAsync(entityToUpdate);

                        int effectedRows = await _unitOfWork.CommitAsync();
                        if (effectedRows != 0)
                        {
                            return new Response<bool>(true);
                        }
                    }
                }

                return new Response<bool>(SD.ErrorOccurred);
            }

            return new Response<bool>("not authorized");
        }


        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            if (_permissionChecker.HasClaim(AppPermissions.BookCategory.Delete))
            {
                await _unitOfWork.Repository<IBookCategoryRepositoryAsync>().DeleteAsync(id);

                int effectedRows = await _unitOfWork.CommitAsync();
                if (effectedRows != 0)
                {
                    return new Response<bool>(true);
                }

                return new Response<bool>(SD.ErrorOccurred);
            }

            return new Response<bool>("not authorized");
        }


        public async Task<Response<BookCategoryReadDto>> GetByIdAsync(Guid id)
        {
            if (_permissionChecker.HasClaim(AppPermissions.AppClaim.View))
            {
                var result = await _unitOfWork.Repository<IBookCategoryRepositoryAsync>().GetAsync(id);

                return new Response<BookCategoryReadDto>(_mapper.Map<BookCategoryReadDto>(result));
            }

            return new Response<BookCategoryReadDto>("not authorized");
        }

        public async Task<Response<List<BookCategoryReadDto>>> GetAllAsync(Expression<Func<BookCategory, bool>> predicate = null,
            Func<IQueryable<BookCategory>, IOrderedQueryable<BookCategory>> orderBy = null, params Expression<Func<BookCategory, object>>[] includes)
        {
            if (_permissionChecker.HasClaim(AppPermissions.AppClaim.List))
            {
                var result = await _unitOfWork.Repository<IBookCategoryRepositoryAsync>().GetAllAsync(predicate, orderBy, includes);
                return new Response<List<BookCategoryReadDto>>(_mapper.Map<List<BookCategoryReadDto>>(result));
            }

            return new Response<List<BookCategoryReadDto>>("not authorized");
        }


        public async Task<Response<PagedResult<BookCategoryReadDto>>> GetPagedListAsync(Expression<Func<BookCategory, bool>> predicate = null,
            Func<IQueryable<BookCategory>, IOrderedQueryable<BookCategory>> orderBy = null, int pageIndex = 0, int pageSize = 10, params Expression<Func<BookCategory, object>>[] includes)
        {
            if (_permissionChecker.HasClaim(AppPermissions.BookCategory.List))
            {

                var pagedResult = new PagedResult<BookCategoryReadDto>();

                var result = await _unitOfWork.Repository<IBookCategoryRepositoryAsync>().GetPagedListAsync(predicate, orderBy, pageIndex, pageSize, includes);
                if (result != null)
                {
                    pagedResult.TotalCount = result.TotalCount;
                    pagedResult.FilteredTotalCount = result.FilteredTotalCount;

                    if (result.Data != null && result.Data.Count > 0)
                    {
                        pagedResult.Data = _mapper.Map<List<BookCategoryReadDto>>(result.Data);
                    }
                }

                return new Response<PagedResult<BookCategoryReadDto>>(pagedResult);
            }

            return new Response<PagedResult<BookCategoryReadDto>>("not authorized");
        }
    }
}
