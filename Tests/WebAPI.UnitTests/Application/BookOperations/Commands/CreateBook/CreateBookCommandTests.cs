using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebAPI.Application.BookOperations.Commands.CreateBook;
using WebAPI.DBOperations;
using WebAPI.Entities;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
  public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
    {
      // Arrange (hazirlik)
      var book = new Book(){
        Title = "TestTitle",
        PageCount = 130,
        PublishDate = new System.DateTime(1990, 11, 10),
        GenreId = 1
        };
      
      _context.Books.Add(book);
      _context.SaveChanges();

      CreateBookCommand command = new CreateBookCommand(_context, _mapper);
      command.Model = new CreateBookModel()
      {
        Title = "TestTitle"
      };

      // Act (calistirma) & Assert (dogrulama)
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
            .And.Message
            .Should().Be("Kitap zaten mevcut!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
       // Arrange
      CreateBookCommand command = new CreateBookCommand(_context, _mapper);
      CreateBookModel model = new CreateBookModel()
      {
        Title = "Hobbit",
        PageCount = 130,
        PublishDate = DateTime.Now.Date.AddYears(-5),
        GenreId = 1
      };
      command.Model = model;
      
      // Act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // Assert
      var book = _context.Books.SingleOrDefault(
        book => book.Title == model.Title
      );
      book.Should().NotBeNull();
      book.PageCount.Should().Be(model.PageCount);
      book.PublishDate.Should().Be(model.PublishDate);
      book.GenreId.Should().Be(model.GenreId);
    }
  }
}