using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.GenreOperations.Command.CreateGenre;
using WebAPI.Application.GenreOperations.Command.DeleteGenre;
using WebAPI.Application.GenreOperations.Command.UpdateGenre;
using WebAPI.Application.GenreOperations.Queries.GetGenreDetail;
using WebAPI.Application.GenreOperations.Queries.GetGenres;
using WebAPI.DBOperations;

namespace WebAPI.Controllers
{
  [ApiController]
  [Route("[controller]s")]
  public class GenreController : ControllerBase
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GenreController(BookStoreDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetGenres()
    {
      GetGenresQuery query = new GetGenresQuery(_context, _mapper);
      var obj = query.Handle();

      return Ok(obj);
    }

    [HttpGet("id")]
    public IActionResult GetGenreDetail(int id)
    {
      GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
      query.GenreId = id;
      // Validation process
      GetGenreDetailQueryValidation validator = new GetGenreDetailQueryValidation();
      validator.ValidateAndThrow(query);

      var obj = query.Handle();

      return Ok(obj);
    }

    [HttpPost]
    public IActionResult AddGenre([FromBody]CreateGenreModel newGenre)
    {
      CreateGenreCommand command = new CreateGenreCommand(_context);
      command.Model = newGenre;

      CreateGenreCommandValidation validator = new CreateGenreCommandValidation();
      validator.ValidateAndThrow(command);

      command.Handle();

      return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateGenre(int id, [FromBody]UpdateGenreModel updateGenre)
    {
      UpdateGenreCommand command = new UpdateGenreCommand(_context);
      command.GenreId = id;
      command.Model = updateGenre;

      UpdateGenreCommandValidation validator = new UpdateGenreCommandValidation();
      validator.ValidateAndThrow(command);

      command.Handle();

      return Ok();
    }


    [HttpDelete("id")]
    public IActionResult DeleteGenre(int id)
    {
      DeleteGenreCommand command = new DeleteGenreCommand(_context);
      command.GenreId = id;

      DeleteGenreCommandValidation validator = new DeleteGenreCommandValidation();
      validator.ValidateAndThrow(command);

      command.Handle();

      return Ok();
    }
  }
}