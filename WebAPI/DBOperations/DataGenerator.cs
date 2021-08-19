using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.DBOperations
{
  public class DataGenerator
  {
    public static void Initialize(IServiceProvider serviceProvider)
    {
      using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
      {
        // Look for any book.
        if (context.Books.Any())
        {
          return;   // Data was already seeded
        }

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

        context.SaveChanges();
      }
    }
  }
}