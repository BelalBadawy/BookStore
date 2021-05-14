using System;
using BS.Application.Dtos;
using FluentValidation;

namespace BS.Application.Validations
{
    public class BookCategoryUpsertDtoValidator : AbstractValidator<BookCategoryUpsertDto>
    {
        public BookCategoryUpsertDtoValidator()
        {
            RuleFor<Guid>(x => x.Id).NotEqual(Guid.Empty); ;
            RuleFor(o => o.Title).NotEmpty().MaximumLength(150);
        }
    }
}
