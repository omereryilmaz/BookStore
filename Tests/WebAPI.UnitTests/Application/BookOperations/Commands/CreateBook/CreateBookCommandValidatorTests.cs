using System;
using FluentAssertions;
using TestSetup;
using WebAPI.Application.BookOperations.Commands.CreateBook;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
  public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
  {
    [Theory]
    [InlineData("Lord Of Rings", 0, 0)]
    [InlineData("Lord Of Rings", 0, 1)]
    [InlineData("Lord Of Rings", 100, 0)]
    [InlineData("", 0, 0)]
    [InlineData("", 100, 1)]
    [InlineData("", 0, 1)]
    [InlineData("Lor", 100, 1)]
    [InlineData("Lord", 100, 0)]
    [InlineData("Lord", 0, 1)]
    [InlineData(" ", 100, 1)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(
      string title, int pageCount, int genreId
    )
    {
      // Arrange
      CreateBookCommand command = new CreateBookCommand(null, null);
      command.Model = new CreateBookModel()
      {
        Title = title,
        PageCount = pageCount,
        PublishDate = DateTime.Now.Date.AddYears(-1),
        GenreId = genreId
      };

      // Act
      CreateBookCommandValidator validator = new CreateBookCommandValidator();
      var result = validator.Validate(command);

      // Assert
      result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
    {
      // Arrange
      CreateBookCommand command = new CreateBookCommand(null, null);
      command.Model = new CreateBookModel()
      {
        Title = "Lord Of Rings",
        PageCount = 111,
        PublishDate = DateTime.Now.Date,
        GenreId = 1
      };

      // Act
      CreateBookCommandValidator validator = new CreateBookCommandValidator();
      var result = validator.Validate(command);

      // Assert
      result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldNotBeReturnError()
    {
      // Arrange
      CreateBookCommand command = new CreateBookCommand(null, null);
      command.Model = new CreateBookModel()
      {
        Title = "Lord Of Rings",
        PageCount = 111,
        PublishDate = DateTime.Now.Date.AddYears(-1),
        GenreId = 1
      };

      // Act
      CreateBookCommandValidator validator = new CreateBookCommandValidator();
      var result = validator.Validate(command);

      // Assert
      result.Errors.Count.Should().Equals(0);
    }
  }
}