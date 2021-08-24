using System;
using WebAPI.DBOperations;
using WebAPI.Entities;

namespace TestSetup
{
  public static class Books
  {
    public static void AddBooks(this BookStoreDbContext context)
    {
      context.Books.AddRange(
        new Book()
        {
          Title = "Lean Startup",
          GenreId = 1, // Personal Growth
          PageCount = 200,
          PublishDate = new DateTime(2001, 06, 12)
        },
        new Book()
        {
          Title = "Lord Of The Rings",
          GenreId = 2, // ScienceFiction
          PageCount = 1560,
          PublishDate = new DateTime(1996, 08, 11)
        },
        new Book()
        {
          Title = "Harry Potter",
          GenreId = 2, // ScienceFiction
          PageCount = 600,
          PublishDate = new DateTime(2011, 07, 14)
        }
      );
    }
  }
}