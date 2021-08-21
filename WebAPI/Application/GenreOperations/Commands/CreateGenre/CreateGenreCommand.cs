using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;
using WebAPI.Entities;

namespace WebAPI.Application.GenreOperations.Command.CreateGenre
{
  public class CreateGenreCommand
  {
    public CreateGenreModel Model { get; set; }
    private readonly BookStoreDbContext _context;

    public CreateGenreCommand(BookStoreDbContext context)
    {
      _context = context;
    }

    public void Handle()
    {
      // Genre duplicate control
      var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
      if (genre is not null)      
          throw new InvalidOperationException("Kitap Turu Zaten Mevcut!");
      
      genre = new Genre();
      genre.Name = Model.Name;

      _context.Genres.Add(genre);
      _context.SaveChanges();
    }
  }

  public class CreateGenreModel
  {
    public string Name { get; set; }
  }
}