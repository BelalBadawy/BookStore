using BS.Application.Dtos;
using BS.Domain.Entities;

namespace BS.Application.Mapping
{
    public class BookCategoryProfile : AutoMapper.Profile
    {
        public BookCategoryProfile()
        {
            CreateMap<BookCategory, BookCategoryReadDto>().ReverseMap();
            CreateMap<BookCategory, BookCategoryUpsertDto>().ReverseMap();
            CreateMap<BookCategoryReadDto, BookCategoryUpsertDto>().ReverseMap();


        }
    }
}
