using System;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BookOperations.CreateBook;
using WebAPI.BookOperations.DeleteBook;
using WebAPI.BookOperations.GetBookDetail;
using WebAPI.BookOperations.GetBooks;
using WebAPI.BookOperations.UpdateBook;
using WebAPI.DBOperations;

namespace WebAPI.AddControllers
{

  [ApiController]
  [Route("[controller]s")]
  public class BookController : ControllerBase
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public BookController(BookStoreDbContext context, IMapper mapper)
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
      try
      {
        GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
        query.BookId = id;
        var result = query.Handle();
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
      CreateBookCommand command = new CreateBookCommand(_context, _mapper);
      try
      {
        command.Model = newBook;

        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        ValidationResult result = validator.Validate(command);

        validator.ValidateAndThrow(command);

        command.Handle();       
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }

      return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
      try
      {
        UpdateBookCommand command = new UpdateBookCommand(_context);
        command.BookId = id;
        command.Model = updatedBook;
        command.Handle();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }

      return Ok();
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
       try
      {
        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.BookId = id;
        command.Handle();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }

      return Ok();
    }

  }
}