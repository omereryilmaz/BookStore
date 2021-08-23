using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;
using WebAPI.Entities;

namespace WebAPI.Application.BookOperations.Queries.GetBookDetail
{
  public class GetBookDetailQuery
  {
    private readonly BookStoreDbContext _dbContext;
    public int BookId { get; set; }
    private readonly IMapper _mapper;
    public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
      var book = _dbContext.Books.Include(x => x.Genre)
                                 .Where(book => book.Id == BookId)
                                 .SingleOrDefault();
      
      if (book is null)
      {
          throw new InvalidOperationException("Kitap Bulunamadi!");
      }
      
      BookDetailViewModel vm = _mapper.Map<Book, BookDetailViewModel>(book);
      
      return vm;
    }
  }

  public class BookDetailViewModel
  {
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
  }
}
