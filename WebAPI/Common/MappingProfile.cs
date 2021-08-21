using AutoMapper;
using WebAPI.Application.BookOperations.CreateBook;
using WebAPI.Application.BookOperations.GetBookDetail;
using WebAPI.Application.BookOperations.GetBooks;
using WebAPI.Entities;

namespace WebAPI.Common
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<CreateBookModel, Book>();

      CreateMap<Book, BookDetailViewModel>()
            .ForMember(
                dest => dest.Genre, 
                opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));

      CreateMap<Book, BooksViewModel>()
        .ForMember(
                dest => dest.Genre, 
                opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
    }
  }
}