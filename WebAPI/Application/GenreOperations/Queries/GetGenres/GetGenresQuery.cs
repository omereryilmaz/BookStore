using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;

namespace WebAPI.Application.GenreOperations.Queries.GetGenres
{
  public class GetGenresQuery
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public List<GenresViewModel> Handle()
    {
      // Get only Genres that have isActive = true
      var genres = _context.Genres
                      .Where(x => x.IsActive == true)
                      .OrderBy(x => x.Id);
      // Mapping to GenresViewModel from genres
      List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
    
      return returnObj;
    }
  }

  public class GenresViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}