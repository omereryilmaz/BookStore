using System;
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
  }
}