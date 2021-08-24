using System;
using System.Linq;
using WebAPI.DBOperations;

namespace WebAPI.Application.GenreOperations.Command.DeleteGenre
{
  public class DeleteGenreCommand
  {
    public int GenreId { get; set; }
    private readonly IBookStoreDbContext _context;

    public DeleteGenreCommand(IBookStoreDbContext context)
    {
      _context = context;
    }

    public void Handle()
    {
      // Find genre for remove operation
      var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
      if (genre is null)      
          throw new InvalidOperationException("Kitap Turu Bulunamadi!");
      
      _context.Genres.Remove(genre);
      _context.SaveChanges();
    }
  }
}