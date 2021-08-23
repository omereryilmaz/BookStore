using AutoMapper;
using WebAPI.Application.BookOperations.Commands.CreateBook;
using WebAPI.Application.BookOperations.Queries.GetBookDetail;
using WebAPI.Application.BookOperations.Queries.GetBooks;
using WebAPI.Application.GenreOperations.Queries.GetGenreDetail;
using WebAPI.Application.GenreOperations.Queries.GetGenres;
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
                opt => opt.MapFrom(src => src.Genre.Name));

      CreateMap<Book, BooksViewModel>()
        .ForMember(
                dest => dest.Genre, 
                opt => opt.MapFrom(src => src.Genre.Name));
   
      CreateMap<Genre, GenresViewModel>();  
      CreateMap<Genre, GenreDetailViewModel>(); 
    }
  }
}