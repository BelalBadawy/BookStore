using AutoMapper;
using BS.Application.Dtos;
using BS.Application.Interfaces;
using BS.Application.Interfaces.Repositories;
using BS.Application.Services.Interfaces;
using BS.Application.Validations;
using BS.Domain.Common;
using BS.Domain.Entities;
using FluentValidation.Results;
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

        public BookCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> AddAsync(BookCategoryUpsertDto bookCategoryUpsertDto)
        {
            BookCategoryUpsertDtoValidator dtoValidator = new BookCategoryUpsertDtoValidator();

            ValidationResult validationResult = dtoValidator.Validate(bookCategoryUpsertDto);

            if (validationResult != null && validationResult.IsValid == false)
            {
                return new Response<Guid>(validationResult.Errors.Select(modelError => modelError.ErrorMessage).ToList());
            }
            else
            {
                if (await _unitOfWork.Repository<IBookCategoryRepositoryAsync>().AnyAsync(o => o.Title.ToUpper() == bookCategoryUpsertDto.Title.ToUpper()))
                {
                    return new Response<Guid>(string.Format(SD.ExistData, bookCategoryUpsertDto.Title));
                }
                else
                {
                    BookCategory bookCategory = _mapper.Map<BookCategory>(bookCategoryUpsertDto);

                    var addedEntity = await _unitOfWork.Repository<IBookCategoryRepositoryAsync>().AddAsync(bookCategory);

                    int effectedRows = await _unitOfWork.CommitAsync();
                    if (effectedRows != 0)
                    {
                        return new Response<Guid>(addedEntity.Id);
                    }
                }
            }

            return new Response<Guid>(SD.ErrorOccurred);
        }

        public async Task<Response<bool>> UpdateAsync(BookCategoryUpsertDto bookCategoryUpsertDto)
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
                    BookCategory bookCategory = _mapper.Map<BookCategory>(bookCategoryUpsertDto);

                    await _unitOfWork.Repository<IBookCategoryRepositoryAsync>().UpdateAsync(bookCategory);

                    int effectedRows = await _unitOfWork.CommitAsync();
                    if (effectedRows != 0)
                    {
                        return new Response<bool>(true);
                    }
                }
            }

            return new Response<bool>(SD.ErrorOccurred);
        }


        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            await _unitOfWork.Repository<IBookCategoryRepositoryAsync>().DeleteAsync(id);

            int effectedRows = await _unitOfWork.CommitAsync();
            if (effectedRows != 0)
            {
                return new Response<bool>(true);
            }

            return new Response<bool>(SD.ErrorOccurred);
        }

        public async Task<BookCategoryReadDto> GetByIdAsync(Guid id)
        {
            var result = await _unitOfWork.Repository<IBookCategoryRepositoryAsync>().GetAsync(id);

            return _mapper.Map<BookCategoryReadDto>(result);
        }

        public async Task<List<BookCategoryReadDto>> GetAllAsync(Expression<Func<BookCategory, bool>> predicate = null,
            Func<IQueryable<BookCategory>, IOrderedQueryable<BookCategory>> orderBy = null, params Expression<Func<BookCategory, object>>[] includes)
        {
            var result = await _unitOfWork.Repository<IBookCategoryRepositoryAsync>().GetAllAsync(predicate, orderBy, includes);
            return _mapper.Map<List<BookCategoryReadDto>>(result);
        }

    }
}
