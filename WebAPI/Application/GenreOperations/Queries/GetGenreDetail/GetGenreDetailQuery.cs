using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;

namespace WebAPI.Application.GenreOperations.Queries.GetGenreDetail
{
  public class GetGenreDetailQuery
  {
    public int GenreId { get; set; }
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public GenreDetailViewModel Handle()
    {
      // Get only Genres that have isActive = true and Id = GenreID
      var genre = _context.Genres
                      .Where(x => x.IsActive == true && x.Id == GenreId);
      
      if (genre is null)
      {
          throw new InvalidOperationException("Kitap Turu Bulunamadi!");
      }
                      
      // Mapping to GenreDetailViewModel from genres
      GenreDetailViewModel returnObj = _mapper.Map<GenreDetailViewModel>(genre);
    
      return returnObj;
    }
  }

  public class GenreDetailViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}