using AutoMapper;
using WebAPI.BookOperations.CreateBook;
using WebAPI.BookOperations.GetBookDetail;
using WebAPI.BookOperations.GetBooks;

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