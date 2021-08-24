using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;
using WebAPI.Entities;

namespace WebAPI.Application.GenreOperations.Command.UpdateGenre
{
  public class UpdateGenreCommand
  {
    public int GenreId { get; set; }
    public UpdateGenreModel Model { get; set; }
    private readonly IBookStoreDbContext _context;

    public UpdateGenreCommand(IBookStoreDbContext context)
    {
      _context = context;
    }

    public void Handle()
    {
      // Genre duplicate control
      var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
      if (genre is null)      
        throw new InvalidOperationException("Kitap turu bulunamadi!");
      
      if (_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
        throw new InvalidOperationException("Ayni isme sahip bir kitap turu zaten mevcut!");
      
      // If Name field has not been changed then it stay the same
      genre.Name = Model.Name.Trim() != default ? Model.Name : genre.Name;
      genre.IsActive = Model.IsActive != default ? Model.IsActive : genre.IsActive;
      _context.SaveChanges();
    }
  }

  public class UpdateGenreModel
  {
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
  }
}