using System;
using System.Linq;
using WebAPI.Common;
using WebAPI.DBOperations;

namespace WebAPI.Application.BookOperations.DeleteBook
{
  public class DeleteBookCommand
  {
    private readonly BookStoreDbContext _dbContext;

    public int BookId { get; set; }

    public DeleteBookCommand(BookStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Handle()
    {
      var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

      if (book is null)
        throw new InvalidOperationException("Kitap Bulunamadi!");

      _dbContext.Books.Remove(book);
      _dbContext.SaveChanges();
    }
  }
}
