using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.DBOperations
{
  public interface IBookStoreDbContext
  {
    DbSet<Book> Books {get; set;}
    DbSet<Genre> Genres {get; set;}
    int SaveChanges();
  }
}