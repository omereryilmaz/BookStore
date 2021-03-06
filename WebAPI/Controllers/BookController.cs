using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.BookOperations.Commands.CreateBook;
using WebAPI.Application.BookOperations.Commands.DeleteBook;
using WebAPI.Application.BookOperations.Queries.GetBookDetail;
using WebAPI.Application.BookOperations.Queries.GetBooks;
using WebAPI.Application.BookOperations.Commands.UpdateBook;
using WebAPI.DBOperations;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]s")]
  public class BookController : ControllerBase
  {
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public BookController(IBookStoreDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
      GetBooksQuery query = new GetBooksQuery(_context, _mapper);
      var result = query.Handle();

      return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
      query.BookId = id;
      // Model validation process
      GetBookDetailQueryValidation validator = new GetBookDetailQueryValidation();
      // If validation result is not valid, throw exception - FluentValidation
      validator.ValidateAndThrow(query);
    
      var result = query.Handle();

      return Ok(result);
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
      CreateBookCommand command = new CreateBookCommand(_context, _mapper);
      command.Model = newBook;
      // Model validation process
      CreateBookCommandValidator validator = new CreateBookCommandValidator();
      // If validation result is not valid, throw exception - FluentValidation
      validator.ValidateAndThrow(command);

      command.Handle();

      return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {

      UpdateBookCommand command = new UpdateBookCommand(_context);
      command.BookId = id;
      command.Model = updatedBook;
      // Model validation process
      UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
      // If validation result is not valid, throw exception - FluentValidation
      validator.ValidateAndThrow(command);
      command.Handle();

      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
      DeleteBookCommand command = new DeleteBookCommand(_context);
      command.BookId = id;
      // Model validation process
      DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
      // If validation result is not valid, throw exception - FluentValidation
      validator.ValidateAndThrow(command);
  
      command.Handle();

      return Ok();
    }
  }
}